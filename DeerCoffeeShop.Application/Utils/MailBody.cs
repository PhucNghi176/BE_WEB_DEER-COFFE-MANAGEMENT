namespace DeerCoffeeShop.Application.Utils
{
    public static class MailBody
    {

        public static string getConfirmEmail(string userName, string userEmail, string address, string phoneNumber, string dateOfBirth, string companyName)
        {
            string htmlTemplate = @"
                <!DOCTYPE html>
                <html lang='vi'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Thư Xác Nhận Đăng Ký Thành Công</title>
                    <style type='text/css'>
                        body {
                            margin: 0;
                            padding: 0;
                            font-family: Arial, sans-serif;
                            background-color: #f9f9f9;
                            color: #333333;
                        }
                        table {
                            border-spacing: 0;
                            width: 100%;
                        }
                        td {
                            padding: 0;
                        }
                        img {
                            border: 0;
                        }
                        .container {
                            width: 100%;
                            max-width: 600px;
                            margin: 0 auto;
                            background-color: #ffffff;
                            border-radius: 10px;
                            overflow: hidden;
                            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
                        }
                        .header {
                            background-color: #0073e6;
                            color: #ffffff;
                            text-align: center;
                            padding: 20px 0;
                        }
                        .header img {
                            max-width: 200px;
                            height: auto;
                        }
                        .header h1 {
                            margin: 0;
                            font-size: 24px;
                        }
                        .content {
                            padding: 20px;
                            text-align: left;
                        }
                        .content h2 {
                            font-size: 22px;
                            margin: 0 0 10px;
                        }
                        .content p {
                            font-size: 16px;
                            line-height: 1.6;
                            margin: 10px 0;
                        }
                        .footer {
                            background-color: #f1f1f1;
                            text-align: center;
                            padding: 15px 0;
                            font-size: 14px;
                            color: #777777;
                        }
                        .footer a {
                            color: #0073e6;
                            text-decoration: none;
                        }
                        .social-links {
                            margin: 10px 0;
                        }
                        .social-links img {
                            width: 32px;
                            height: 32px;
                            margin: 0 5px;
                        }
                        @media only screen and (max-width: 600px) {
                            .container {
                                width: 100%;
                            }
                        }
                    </style>
                </head>
                <body>
                    <table role='presentation' class='container'>
                        <!-- Header Section -->
                        <tr>
                            <td class='header'>
                                <img style='border-radius: 30%; overflow: hidden; padding: 10px; text-align: center;' src='https://lh3.google.com/u/0/d/1sfCMCqs5fGIfQvM4CJwWclx-d0jzaf13=w2548-h1850-iv1' alt='Company Logo' style='width: 100%; height: auto;'>
                                <h1 style='margin: 0;'>Xác Nhận Đăng Ký Thành Công</h1>
                            </td>
                        </tr>
                        
                        <!-- Content Section -->
                        <tr>
                            <td class='content'>
                                <h2>Chào mừng, {{user_name}}!</h2>
                                <p>Cảm ơn bạn đã đăng ký để trở thành thành viên của Deer Coffee. Dưới đây là thông tin chi tiết về tài khoản của bạn:</p>
                                <p><strong>Tên người dùng:</strong> {{user_name}}</p>
                                <p><strong>Email:</strong> {{user_email}}</p>
                                <p><strong>Địa chỉ:</strong> {{user_address}}</p>
                                <p><strong>Ngày sinh:</strong> {{user_dateOfBirth}}</p>
                                <p><strong>Số điện thoại:</strong> {{user_phoneNumber}}</p>
                                <p style='font-style: italic' >Chúng tôi sẽ liên lạc với bạn sớm nhất để phỏng vấn. Vui lòng chú ý email !</p>
                                <p>Nếu bạn có bất kỳ câu hỏi nào, đừng ngần ngại liên hệ với chúng tôi qua <a href='mailto:deer.coffee.hr@gmail.com' style='color: #0073e6;'>deer.coffee.hr@gmail.com</a>.</p>
                                <p>Trân trọng,<br>Đội ngũ {{company_name}}</p>.
                            </td>
                        </tr>
                        
                        <!-- Footer Section -->
                        <tr>
                            <td class='footer'>
                                <p>Bạn nhận được email này vì đã đăng ký tại {{company_name}}.</p>
                                <div class='social-links'>
                                    <a href='https://www.facebook.com/bentran1vn'>Facebook</a>
                                    <a href='https://www.facebook.com/bentran1vn'>Twitter</a>
                                    <a href='https://www.instagram.com/bentran1vn'>Instagram</a>
                                </div>
                                <p><a href='https://yourcompany.com/unsubscribe'>Hủy đăng ký</a> | <a href='http://localhost:3000'>Trang chủ</a></p>
                            </td>
                        </tr>
                    </table>
                </body>
                </html>
                ";

            // Replace placeholders with actual content
            htmlTemplate = htmlTemplate.Replace("{{user_name}}", userName);
            htmlTemplate = htmlTemplate.Replace("{{user_email}}", userEmail);
            htmlTemplate = htmlTemplate.Replace("{{user_address}}", address);
            htmlTemplate = htmlTemplate.Replace("{{user_dateOfBirth}}", dateOfBirth);
            htmlTemplate = htmlTemplate.Replace("{{user_phoneNumber}}", phoneNumber);
            htmlTemplate = htmlTemplate.Replace("{{company_name}}", companyName);

            return htmlTemplate;
        }
        public static string getApprovedEmail(string userName, string userEmail, string companyName, DateTime date, string company_address)
        {
            string htmlTemplate = @"
                <!DOCTYPE html>
                <html lang='vi'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Thư Xác Nhận Phỏng Vấn</title>
                    <style type='text/css'>
                        body {
                            margin: 0;
                            padding: 0;
                            font-family: Arial, sans-serif;
                            background-color: #f9f9f9;
                            color: #333333;
                        }
                        table {
                            border-spacing: 0;
                            width: 100%;
                        }
                        td {
                            padding: 0;
                        }
                        img {
                            border: 0;
                        }
                        .container {
                            width: 100%;
                            max-width: 600px;
                            margin: 0 auto;
                            background-color: #ffffff;
                            border-radius: 10px;
                            overflow: hidden;
                            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
                        }
                        .header {
                            background-color: #0073e6;
                            color: #ffffff;
                            text-align: center;
                            padding: 20px 0;
                        }
                        .header img {
                            max-width: 200px;
                            height: auto;
                        }
                        .header h1 {
                            margin: 0;
                            font-size: 24px;
                        }
                        .content {
                            padding: 20px;
                            text-align: left;
                        }
                        .content h2 {
                            font-size: 22px;
                            margin: 0 0 10px;
                        }
                        .content p {
                            font-size: 16px;
                            line-height: 1.6;
                            margin: 10px 0;
                        }
                        .footer {
                            background-color: #f1f1f1;
                            text-align: center;
                            padding: 15px 0;
                            font-size: 14px;
                            color: #777777;
                        }
                        .footer a {
                            color: #0073e6;
                            text-decoration: none;
                        }
                        .social-links {
                            margin: 10px 0;
                        }
                        .social-links img {
                            width: 32px;
                            height: 32px;
                            margin: 0 5px;
                        }
                        @media only screen and (max-width: 600px) {
                            .container {
                                width: 100%;
                            }
                        }
                    </style>
                </head>
                <body>
                    <table role='presentation' class='container'>
                        <!-- Header Section -->
                        <tr>
                            <td class='header'>
                                <img style='border-radius: 30%; overflow: hidden; padding: 10px; text-align: center;' src='https://lh3.google.com/u/0/d/1sfCMCqs5fGIfQvM4CJwWclx-d0jzaf13=w2548-h1850-iv1' alt='Company Logo' style='width: 100%; height: auto;'>
                                <h1 style='margin: 0;'>Xác Nhận Phỏng Vấn Thành Công</h1>
                            </td>
                        </tr>
                        
                        <!-- Content Section -->
                        <tr>
                            <td class='content'>
                                <h2>Chào mừng, {{user_name}}!</h2>
                                <p>Cảm ơn bạn đã đăng ký để trở thành thành viên của Deer Coffee. Dưới đây là thông tin chi tiết của bạn:</p>
                                <p><strong>Tên người dùng:</strong> {{user_name}}</p>
                                <p><strong>Email:</strong> {{user_email}}</p>
                                <p style='font-style: italic' >Chúng tôi xin gửi bạn lịch phỏng vấn. Vui lòng chú ý email !</p>
                                <p><strong>Thời gian phỏng vấn:</strong> {{date}}</p>
                                <p><strong>Địa điểm:</strong> {{company_address}}</p>
                                <p>Nếu bạn có bất kỳ câu hỏi nào, đừng ngần ngại liên hệ với chúng tôi qua <a href='mailto:deer.coffee.hr@gmail.com' style='color: #0073e6;'>deer.coffee.hr@gmail.com</a>.</p>
                                <p>Trân trọng,<br>Đội ngũ {{company_name}}</p>.
                            </td>
                        </tr>
                        
                        <!-- Footer Section -->
                        <tr>
                            <td class='footer'>
                                <p>Bạn nhận được email này vì đã đăng ký tại {{company_name}}.</p>
                                <div class='social-links'>
                                    <a href='https://www.facebook.com/bentran1vn'>Facebook</a>
                                    <a href='https://www.facebook.com/bentran1vn'>Twitter</a>
                                    <a href='https://www.instagram.com/bentran1vn'>Instagram</a>
                                </div>
                                <p><a href='https://yourcompany.com/unsubscribe'>Hủy đăng ký</a> | <a href='http://localhost:3000'>Trang chủ</a></p>
                            </td>
                        </tr>
                    </table>
                </body>
                </html>
                ";

            // Replace placeholders with actual content
            htmlTemplate = htmlTemplate.Replace("{{user_name}}", userName);
            htmlTemplate = htmlTemplate.Replace("{{user_email}}", userEmail);
            htmlTemplate = htmlTemplate.Replace("{{date}}", date.ToString());
            htmlTemplate = htmlTemplate.Replace("{{company_name}}", companyName);
            htmlTemplate = htmlTemplate.Replace("{{company_name}}", company_address);


            return htmlTemplate;
        }
        public static string getPasswordEmail(string userName, string userID, DateTime date, string companyName)
        {
            string htmlTemplate = @"
                <!DOCTYPE html>
                <html lang='vi'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Đơn Thông Tin Tài Khoản Đăng Nhập</title>
                    <style type='text/css'>
                        body {
                            margin: 0;
                            padding: 0;
                            font-family: Arial, sans-serif;
                            background-color: #f9f9f9;
                            color: #333333;
                        }
                        table {
                            border-spacing: 0;
                            width: 100%;
                        }
                        td {
                            padding: 0;
                        }
                        img {
                            border: 0;
                        }
                        .container {
                            width: 100%;
                            max-width: 600px;
                            margin: 0 auto;
                            background-color: #ffffff;
                            border-radius: 10px;
                            overflow: hidden;
                            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
                        }
                        .header {
                            background-color: #0073e6;
                            color: #ffffff;
                            text-align: center;
                            padding: 20px 0;
                        }
                        .header img {
                            max-width: 200px;
                            height: auto;
                        }
                        .header h1 {
                            margin: 0;
                            font-size: 24px;
                        }
                        .content {
                            padding: 20px;
                            text-align: left;
                        }
                        .content h2 {
                            font-size: 22px;
                            margin: 0 0 10px;
                        }
                        .content p {
                            font-size: 16px;
                            line-height: 1.6;
                            margin: 10px 0;
                        }
                        .footer {
                            background-color: #f1f1f1;
                            text-align: center;
                            padding: 15px 0;
                            font-size: 14px;
                            color: #777777;
                        }
                        .footer a {
                            color: #0073e6;
                            text-decoration: none;
                        }
                        .social-links {
                            margin: 10px 0;
                        }
                        .social-links img {
                            width: 32px;
                            height: 32px;
                            margin: 0 5px;
                        }
                        @media only screen and (max-width: 600px) {
                            .container {
                                width: 100%;
                            }
                        }
                    </style>
                </head>
                <body>
                    <table role='presentation' class='container'>
                        <!-- Header Section -->
                        <tr>
                            <td class='header'>
                                <img style='border-radius: 30%; overflow: hidden; padding: 10px; text-align: center;' src='https://lh3.google.com/u/0/d/1sfCMCqs5fGIfQvM4CJwWclx-d0jzaf13=w2548-h1850-iv1' alt='Company Logo' style='width: 100%; height: auto;'>
                                <h1 style='margin: 0;'>Đơn Thông Tin Tài Khoản Đăng Nhập</h1>
                            </td>
                        </tr>
                        
                        <!-- Content Section -->
                        <tr>
                            <td class='content'>
                                <h2>Chào mừng, {{user_name}}!</h2>
                                <p>Cảm ơn bạn đã trở thành thành viên của Deer Coffee. Dưới đây là thông tin chi tiết của bạn:</p>
                                <p><strong>Tên người dùng:</strong> {{user_name}}</p>
                                <p><strong>EmployeeID:</strong> {{userID}}</p>
                                 <p><strong>Password:</strong> 123456</p>
                                <p>Nếu bạn có bất kỳ câu hỏi nào, đừng ngần ngại liên hệ với chúng tôi qua <a href='mailto:deer.coffee.hr@gmail.com' style='color: #0073e6;'>deer.coffee.hr@gmail.com</a>.</p>
                                <p>Trân trọng,<br>Đội ngũ {{company_name}}</p>.
                            </td>
                        </tr>
                        
                        <!-- Footer Section -->
                        <tr>
                            <td class='footer'>
                                <p>Bạn nhận được email này vì đã đăng ký tại {{company_name}}.</p>
                                <div class='social-links'>
                                    <a href='https://www.facebook.com/bentran1vn'>Facebook</a>
                                    <a href='https://www.facebook.com/bentran1vn'>Twitter</a>
                                    <a href='https://www.instagram.com/bentran1vn'>Instagram</a>
                                </div>
                                <p><a href='https://yourcompany.com/unsubscribe'>Hủy đăng ký</a> | <a href='http://localhost:3000'>Trang chủ</a></p>
                            </td>
                        </tr>
                    </table>
                </body>
                </html>
                ";

            // Replace placeholders with actual content
            htmlTemplate = htmlTemplate.Replace("{{user_name}}", userName);
            htmlTemplate = htmlTemplate.Replace("{{userID}}", userID);
            htmlTemplate = htmlTemplate.Replace("{{date}}", date.ToString());
            htmlTemplate = htmlTemplate.Replace("{{company_name}}", companyName);

            return htmlTemplate;
        }


    }
}