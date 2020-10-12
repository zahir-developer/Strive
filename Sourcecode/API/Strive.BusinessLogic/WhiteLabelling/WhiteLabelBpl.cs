﻿using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.Model;
using Strive.BusinessLogic.Document;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.WhiteLabelling
{
    public class WhiteLabelBpl : Strivebase, IWhiteLabelBpl
    {
        public WhiteLabelBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }
        public Result AddWhiteLabelling(WhiteLabel whiteLabel)
        {
            string error = string.Empty;
            (error, whiteLabel.LogoPath, whiteLabel.ThumbFileName) = UploadImage(whiteLabel.Base64, whiteLabel.FileName);

            if (error == string.Empty)
            {
                return ResultWrap(new WhiteLabelRal(_tenant).AddWhiteLabelling, whiteLabel, "Status");
            }
            else
            {
                return Helper.ErrorMessageResult(error);
            }
        }
        public Result UpdateWhiteLabelling(WhiteLabel whiteLabel)
        {
            string error = string.Empty;
            (error, whiteLabel.LogoPath, whiteLabel.ThumbFileName) = UploadImage(whiteLabel.Base64, whiteLabel.FileName);

            if (error == string.Empty)
            {
                return ResultWrap(new WhiteLabelRal(_tenant).UpdateWhiteLabelling, whiteLabel, "Status");
            }
            else
            {
                return Helper.ErrorMessageResult(error);
            }
        }
        public Result GetAll()
        {
            var whiteLabel = new WhiteLabelRal(_tenant).GetAll();
            if (!string.IsNullOrEmpty(whiteLabel.WhiteLabel.LogoPath))
                whiteLabel.WhiteLabel.Base64 = new DocumentBpl(_cache, _tenant).GetBase64(GlobalUpload.UploadFolder.LOGO, whiteLabel.WhiteLabel.LogoPath);
            return ResultWrap(whiteLabel, "WhiteLabelling");
        }

        public Result SaveTheme(Themes themes)
        {
            return ResultWrap(new WhiteLabelRal(_tenant).SaveTheme, themes, "Status");
        }
        public (string, string, string) UploadImage(string base64, string fileName)
        {
            string thumbFileName = string.Empty;
            string error = string.Empty;
            if (!string.IsNullOrEmpty(base64) && !string.IsNullOrEmpty(fileName))
            {
                var documentBpl = new DocumentBpl(_cache, _tenant);
                error = documentBpl.ValidateFileFormat(GlobalUpload.UploadFolder.LOGO, fileName);

                if (error != string.Empty)
                    return (error, fileName, thumbFileName);
                (fileName) = documentBpl.Upload(GlobalUpload.UploadFolder.LOGO, base64, fileName);
                if (fileName == string.Empty)
                {
                    return (error, string.Empty, string.Empty);
                }
                else
                {
                    string extension = Path.GetExtension(fileName);
                    thumbFileName = fileName.Replace(extension, string.Empty) + "Thumb" + extension;
                    try
                    {
                        documentBpl.SaveThumbnail(_tenant.LogoThumbWidth, _tenant.LogoThumbHeight, base64, thumbFileName);

                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return (error, fileName, thumbFileName);
        }
    }
}
