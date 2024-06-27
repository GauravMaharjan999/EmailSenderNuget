using InfoDev.IOffice.Helpers;
using InfoDev.IOffice.ICommon.Others;


var mailsettings = new MailSettings
{
    Mail = "sudipshrestha960@gmail.com",
    Password = "lucdxlvshdjpqzdy",
    Host = "smtp.gmail.com",
    Port = 587,
    DisplayName = "Gaurav Maharjan"



};
var emailSender = new EmailSender(mailsettings);


await emailSender.SendEmailAsync("gauravmaharjan000@gmail.com", "Test mail for nuget ", "<p> heklpoasdjfiohasio </p>");