using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazkarti.Core.Services;

namespace Tazkarti.Service
{
    public class QRCodeService : IQRCodeService
    {
        public async Task<string> GenerateQR(string info)
        {
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(info, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qRCode = new PngByteQRCode(qRCodeData);
            byte[] qrCodePng = qRCode.GetGraphic(20);
            string fileName = $"{Guid.NewGuid()}.png";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "qrcodes", fileName);
            string directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            await File.WriteAllBytesAsync(filePath, qrCodePng);
            return $"/qrcodes/{fileName}";
        }
    }
}
