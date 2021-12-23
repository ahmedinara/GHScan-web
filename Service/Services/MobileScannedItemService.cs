using AutoMapper;
using Core.Entities;
using Core.Models;
using Core.Repository;
using Core.Service;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class MobileScannedItemService : IMobileScannedItemService
    {
        private readonly IMobileScannedItemRepository _mobileScannedItemRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public MobileScannedItemService(IMobileScannedItemRepository mobileScannedItemRepository,IMapper mapper,
            IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _mobileScannedItemRepository = mobileScannedItemRepository;
            _mapper = mapper;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        private string GetCodeFormat(string qrCode)
        {
            var sameCode = qrCode;
            string GTIN = "", 
                SN = "",
                BN = "",
                XD = "";
            //if (qrCode.Substring(0, 1) == ",")
                qrCode =qrCode.Replace(",",null);

            if (qrCode.Substring(0, 2) == "01" && qrCode.Length>14)
                qrCode = qrCode.Substring(2, qrCode.Length - 2);
            else
                return null;

            try
            {
                GTIN = qrCode.Substring(0, 14);
            }
            finally
            {
                qrCode = qrCode.Substring(14, qrCode.Length - 14);

            }
            var FristTwoNo = qrCode.Substring(0, 2);
            while (qrCode != null)
            {
                (string value, string type, string newQrCode) switchCase = getvalue(FristTwoNo, qrCode,SN,BN,GTIN);
                switch (switchCase.type)
                {
                    case "SN":
                        SN = switchCase.value;
                        break;
                    case "BN":
                        BN = switchCase.value;
                        break;
                    case "XD":
                        XD = switchCase.value;
                        break;
                }
                qrCode = switchCase.newQrCode;
                if(qrCode !=null)
                FristTwoNo = qrCode.Substring(0, 2);
            }

            return GTIN+";"+SN+";"+ BN+";"+ XD;
        }

        private (string,string,string) getvalue(string fristtwo,string qrCode, 
            string SN ,
             string BN ,
             string XD )
        {
            
            var newqr = qrCode;

            switch (fristtwo)
            {
                case "21":
                    int index = 0;
                    if(!string.IsNullOrEmpty(BN)&& !string.IsNullOrEmpty(XD))
                    {
                        qrCode = qrCode.Substring(2, qrCode.Length - 2);
                        SN = qrCode;
                        return (SN, "SN", null);
                    }
                    else
                    {    while (index <1)
                    {
                        newqr = qrCode;
                        var index1 = newqr.IndexOf("10");
                        if (index1 <= 0)
                            index = qrCode.Length;
                        else
                        {
                            //newqr = qrCode.Substring(index1 + 2, qrCode.Length - (index1 + 2));
                            newqr = newqr.Substring(index1, newqr.Length - (index1));
                            var o = newqr.Substring(2, newqr.Length - 2);
                            var index2 = o.IndexOf("10");
                            if (index2 >= 0)
                            {
                                newqr = newqr.Substring(2, newqr.Length - 2);
                            }
                        }
                    }
                    }
                 
                   
                    qrCode = newqr;
                    return (SN, "SN", qrCode);
                    SN = qrCode.Substring(2, qrCode.Length - (newqr.Length + 2));
                    if ((qrCode.Length - (SN.Length + 2)) == 0)
                        return (SN, "SN", null);

                    qrCode = newqr;

                    return (SN, "SN", qrCode);
                    break;
                case "10":
                    int index10 = 0;
                    //var newqr = qrCode;
                    newqr = qrCode;
                    if (!string.IsNullOrEmpty(SN) && !string.IsNullOrEmpty(XD))
                    {
                        qrCode = qrCode.Substring(2, qrCode.Length - 2);
                        BN = qrCode;
                        return (BN, "BN", null);
                    }
                    while (index10 <1)
                    {
                        var index1 = newqr.IndexOf("21");
                        if (index1 <= 0)
                            index10 = qrCode.Length;
                        else
                        {
                           // newqr = newqr.Substring(index1 + 2, newqr.Length - (index1 + 2));
                            newqr = newqr.Substring(index1 , newqr.Length - (index1 ));
                            var o = newqr.Substring(2, newqr.Length - 2);
                            var index2 = o.IndexOf("21");
                            if (index2 >= 0) 
                            {
                                newqr = newqr.Substring(2, newqr.Length - 2);
                            }
                        }
                    }
                    BN = qrCode.Substring(2, qrCode.Length - (newqr.Length+2));
                    if((qrCode.Length - (BN.Length + 2))==0)
                        return (BN, "BN", null);

                    qrCode = newqr;

                    return (BN, "BN", qrCode);
                    break;
                case "17":
                    string year = qrCode.Substring(2, 2);
                    string month = qrCode.Substring(4, 2);
                    string day = qrCode.Substring(6, 2);
                    XD = (day + "/" + month + "/" + DateTime.Now.ToString("yyyy").Substring(0, 2) + year).ToString();
                    qrCode = qrCode.Substring(8, qrCode.Length-8);
                    return (XD, "XD", qrCode);
                    break;
                default:
                    return (null,null,null);
            }

        }

        #region Add
        public async Task<(string,Guid)> AddScannedItems(List<MobileScannedItemModel> mobileScannedItemModels,int userid)
        {
            Guid guid = Guid.NewGuid();
            var mobileexists = await _mobileScannedItemRepository.GetMobileScannedItemsAsync(false);
            if (mobileexists.Count > 0)
                guid = mobileexists.FirstOrDefault().ScannedGuid;
        
            var mobileScannedItems = mobileScannedItemModels.Select(s => new MobileScannedItem
            {
                CreatedBy = userid,
                CreatedOn = DateTime.Now,
                IsFinshed = false,
                QrCode = s.QrCode,
                FormatedQrCode = GetCodeFormat(s.QrCode),
                ScannedGuid = guid
            }).ToList();

            var entity = await _mobileScannedItemRepository.AddMobileScannedItem(mobileScannedItems);
            var x = _configuration.GetSection("url");
            return (x.Value.ToString() + "/ScanOrder/View?guid=" + guid.ToString(),guid);
        }
        #endregion

        #region Get
        public async Task<IEnumerable<MobileScannedItem>> GetMobileScannedItemActive()
        {
            return await _mobileScannedItemRepository.GetMobileScannedItemsAsync(false);
        }
        public async Task<IEnumerable<MobileScannedItem>> GetMobileScannedItemActiveWithGuId(Guid guid)
        {
            return await _mobileScannedItemRepository.GetMobileScannedItemsbyGUIdAsync(false, guid);
        }
        public async Task<MobileScannedItem> GetMobileScannedItemById(int id)
        {
            return await _mobileScannedItemRepository.GetMobileScannedItemId(id);
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteMobileScannedItemActive()
        {
            return await _mobileScannedItemRepository.DeleteMobileScannedItemIsFinshed();
        }
        #endregion

    }
}
