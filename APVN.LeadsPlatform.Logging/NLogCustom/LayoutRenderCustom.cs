using APVN.LeadsPlatform.Logging.StaticConfig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using NLog;
using NLog.LayoutRenderers;
using System;
using System.Linq;
using System.Text;

namespace APVN.LeadsPlatform.Logging.NLogCustom
{
    [LayoutRenderer("ip_client")]
    public class IpClientLayoutRenderer : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            var httpContextAccessor = new HttpContextAccessor();
            if (httpContextAccessor.HttpContext == null) return;
            try
            {
                if (!string.IsNullOrEmpty(httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"]))
                    builder.Append(httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"]);
                else
                    builder.Append(httpContextAccessor.HttpContext.Request.HttpContext.Features
                        .Get<IHttpConnectionFeature>().RemoteIpAddress);
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                Console.WriteLine(ex.ToString());
            }
        }
    }

    [LayoutRenderer("app_name")]
    public class ApplicationLayoutRenderer : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            try
            {
                builder.Append(StaticConfiguration.ApplicationStore.Values.FirstOrDefault());
            }
            catch
            {
            }
        }
    }

    [LayoutRenderer("app_id")]
    public class ApplicationIdLayoutRenderer : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            try
            {
                builder.Append(StaticConfiguration.ApplicationStore.Keys.FirstOrDefault());

            }
            catch { }
        }
    }

    [LayoutRenderer("messages_info")]
    public class MessagesInfoLayoutRenderer : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            /*var messageInfo = string.Empty;
            try
            {
                messageInfo = JsonConvert.DeserializeObject<NlogMessageCustom>(logEvent.FormattedMessage).Messages;
            }
            catch
            {
            }

            if (string.IsNullOrEmpty(messageInfo)) messageInfo = logEvent.Message;

            builder.Append(messageInfo);*/
            builder.Append(logEvent.FormattedMessage);
        }
    }

    [LayoutRenderer("params_info")]
    public class ParamsInfoLayoutRenderer : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            /*var paramsInfo = string.Empty;
            try
            {
                paramsInfo = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<NlogMessageCustom>(logEvent.FormattedMessage).Params);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            builder.Append(paramsInfo);*/
            builder.Append(JsonConvert.SerializeObject(logEvent.Parameters));
        }
    }
}