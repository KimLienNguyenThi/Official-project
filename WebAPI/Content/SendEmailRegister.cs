namespace WebAPI.Content
{
    public class SendEmailRegister
    {
        public string SendEmail_Register(int otp, string hoTen)
        {
            string emailBody = $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>OTP Verification</title>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            background-color: #f4f4f4;
                            margin: 0;
                            padding: 0;
                        }}
                        .container {{
                            max-width: 600px;
                            margin: 0 auto;
                            background-color: #ffffff;
                            padding: 20px;
                            border-radius: 8px;
                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        }}
                        .header {{
                            text-align: center;
                            padding: 10px 0;
                            background-color: #007bff;
                            color: #ffffff;
                            border-radius: 8px 8px 0 0;
                        }}
                        .content {{
                            padding: 20px;
                            text-align: center;
                        }}
                        .otp-code {{
                            font-size: 24px;
                            font-weight: bold;
                            color: #007bff;
                            letter-spacing: 5px;
                            margin: 20px 0;
                        }}
                        .footer {{
                            text-align: center;
                            font-size: 12px;
                            color: #888888;
                            padding-top: 10px;
                            border-top: 1px solid #dddddd;
                            margin-top: 20px;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h2>Thư viện ABC</h2>
                        </div>
                        <div class='content'>
                            <p>Chào {hoTen},</p>
                            <p>Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi, mã OTP của bạn là:</p>
                            <div class='otp-code'>{otp}</div>
                            <p>Vui lòng không chia sẻ mã này cho bất cứ ai!</p>
                        </div>
                        <div class='footer'>
                            <p>&copy; 2024 THƯ VIỆN ABC. All rights reserved.</p>
                        </div>
                    </div>
                </body>
                </html>";

            return emailBody;
        }

    }
}
