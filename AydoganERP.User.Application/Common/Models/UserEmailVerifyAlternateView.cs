using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace AydoganERP.User.Application.Common.Models;

public static class UserEmailVerifyAlternateView
{
    public static AlternateView GetAlternateView()
    {
        StringBuilder builder = new StringBuilder();

        ContentType mimeType = new System.Net.Mime.ContentType("text/html");
        AlternateView alternate = AlternateView.CreateAlternateViewFromString(builder.ToString(), mimeType);
        alternate.ContentType.CharSet = Encoding.UTF8.WebName;

        return alternate;
    }
}
