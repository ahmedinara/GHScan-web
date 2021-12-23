using AutoMapper;
using Core.Entities;
using Core.Models;
using Core.Repository;
using Core.Service;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Service.Service
{
    public class ScannedHeaderService : IScannedHeaderService
    {
        private readonly IScannedHeaderRepository _scannedHeaderRepository;
        private readonly IMapper _mapper;
        private readonly IMobileScannedItemRepository _mobileScannedItemRepository;
        private readonly IConfiguration _configuration;
        private readonly ISettingConfigRepository _settingConfigRepository;
        public ScannedHeaderService(ISettingConfigRepository settingConfigRepository,IConfiguration configuration,IScannedHeaderRepository scannedHeaderRepository, IMapper mapper,IMobileScannedItemRepository mobileScannedItemRepository)
        {
            _settingConfigRepository = settingConfigRepository;
            _scannedHeaderRepository = scannedHeaderRepository;
            _mapper = mapper;
            _mobileScannedItemRepository = mobileScannedItemRepository;
            _configuration = configuration;

        }
        public string GetHtmlBody(int itmecount ,string url)
        {

            string body = "<div id='email' style='width: 600px; margin: auto; background: white;'>" +
                       "<table role = 'presentation' border = '0' width = '100%' cellspacing = '0'>" +
                        "<tr>" +
                        "<td bgcolor = '#00A4BD' align = 'center' style = 'color: white;'>" +
                        "<h1 style = 'font-size: 52px; margin:0 0 20px 0; font-family:Avenir;'> Welcome to test Qr Code Scanning </h1>" +
                        "</tr>" +
                       "</td>" +
                       "</table>" +
                       "<table role = 'presentation' border = '0' width = '100%' cellspacing = '0'>" +
                       "<tr>" +
                       "<td style = 'padding: 30px 30px 30px 60px;'>" +
                       "<h2 style = 'font-size: 28px; margin:0 0 20px 0; font-family:Avenir;' > Information </h2>" +
                       "<p style = 'margin:0 0 12px 0;font-size:16px;line-height:24px;font-family:Avenir' > Date : " + DateTime.Now.ToString("MM-dd-yyyy HH:MM tt") + "  </p>" +
                       "<p style = 'margin:0 0 12px 0;font-size:16px;line-height:24px;font-family:Avenir' > Number of Items : " + itmecount + " </p>" +
                       " <p style = 'margin:0;font-size:16px;line-height:24px;font-family:Avenir;'><a href = '"+ url + "' style = 'color:#FF7A59;text-decoration:underline;' > Click Here For Download Exel File</a></ p> " +
                       "</td > </ tr></table>  <table role = 'presentation' border = '0' width = '100%' cellspacing = '0'>" +
                       " </table></div> ";                       
            return body;
        }

        #region Add
        public async Task<ScannedHeader> AddScannedHeader(List<MobileScannedViewModel> mobileScannedItem, int userid, string sentMail, string recivedMain,Guid guid)
        {
            var scannedHeader = new ScannedHeader
            {
                CreatedBy = userid,
                CreatedOn = DateTime.Now,
                CountOfItems = mobileScannedItem.Count,
                MailRecive = recivedMain,
                MailSent = sentMail,
                ScannedGuid = guid,
                ScannedDetials= mobileScannedItem.Select(s=> new ScannedDetial 
                { 
                    QrCode=s.QrCode,
                    QrFormat=s.FormatedQrCode
                }).ToList()
            };
            
            mobileScannedItem = null;
            var entity = await _scannedHeaderRepository.AddScannedHeader(scannedHeader);
            if (entity != null)
            {
                var x = _configuration.GetSection("url");
                string url =x.Value.ToString() + "/ScanOrder/finsh?guid=" + guid.ToString();
                bool deleted = await _mobileScannedItemRepository.DeleteMobileScannedItemIsFinshed();
                string body = GetHtmlBody(scannedHeader.CountOfItems, url);// "Date :" + DateTime.Now.ToString("dd-MM-yyyy HH:mm tt") +
                //    "\nNumber of Items :"+scannedHeader.CountOfItems+
                //    "\nUrl For Downloading Exel :"+ url;
                var isSent = await SendEmailAsync(recivedMain, "Test Qr Code Scanning " + DateTime.Now.ToString("dd-MM-yyyy"), body, null, null, true);
            }

            return entity;
        }
        #endregion

        #region Get
        public async Task<IEnumerable<ScannedHeader>> GetMobileScannedItemActive()
        {
            return await _scannedHeaderRepository.GetScannedHeaderAsync();
        }
        public async Task<ScannedHeader> GetScannedHeaderByGUIdAsync(Guid guid)
        {
            return await _scannedHeaderRepository.GetScannedHeaderByGUIdAsync(guid);
        }
        public async Task<ScannedHeader> GetMobileScannedItemById(int id)
        {
            return await _scannedHeaderRepository.GetScannedHeaderId(id);
        }
        #endregion

        #region Sending Mail
        public async Task<bool> SendEmailAsync(string to, string subject, string body, string cc, string bcc, bool isBodyHtml = false)
        {

            var settingConfigs = await _settingConfigRepository.GetAllSettingConfigs("Mail");
            if (settingConfigs.Count() == 0)
                return false;
            #region Get Email Configurations
            (string host, string from, string password) cofig = await GetEmailConfigurations("smtp.gmail.com", "ahmedinara00@gmail.com", "xdotdschxaxbmuwt");
            #endregion
            if (isBodyHtml)
            {
                body = body.Replace("\r\n", "<br/>");
            }
            SmtpClient mailClient = new SmtpClient(settingConfigs.FirstOrDefault(s => s.SettingKey == "Host").SettingValue);
            mailClient.EnableSsl = true;// changed for // Server does not support secure connections.
            mailClient.Port =int.Parse(settingConfigs.FirstOrDefault(s=>s.SettingKey=="Port").SettingValue);
            mailClient.Credentials = new System.Net.NetworkCredential(settingConfigs.FirstOrDefault(s => s.SettingKey == "From").SettingValue, settingConfigs.FirstOrDefault(s => s.SettingKey == "Password").SettingValue);
            // Create the mail message

            MailMessage mailMessage = new MailMessage(settingConfigs.FirstOrDefault(s => s.SettingKey == "To").SettingValue, to, subject, body);
          
            mailMessage.IsBodyHtml = isBodyHtml;

            try
            {
                mailClient.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                string r = ex.Message;
                return false;
            }
        }
        #endregion

        #region Get Email Configurations
        private async Task<(string host, string from, string password)> GetEmailConfigurations(string host, string from, string password)
        {
            return ("smtp.gmail.com", "ahmedinara00@gmail.com", "xdotdschxaxbmuwt");
        }
        #endregion

        #region Genrate Exel

        #endregion

    }
}
