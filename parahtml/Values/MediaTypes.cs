using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.parahtml
{

    public static class MediaTypes
    {

        public static class Audio
        {
            public static readonly string Wave = "audio/wave";
            public static readonly string WebM = "audio/webm";
            public static readonly string Ogg = "audio/ogg";
        }

        public static class Application
        {
            public static readonly string OctetStream = "application/octet-stream";
            public static readonly string JavaScript = "application/javascript";
            public static readonly string EcmaScript = "application/ecmascript";
            public static readonly string Ogg = "application/ogg";
        }

        public static class Image
        {
            public static readonly string Gif = "image/gif";
            public static readonly string Jpeg = "image/jpeg";
            public static readonly string Png = "image/png";
            public static readonly string Svg = "image/svg+xml";
            public static readonly string Icon = "image/x-icon";
        }

        public static class Multipart
        {
            public static readonly string FormData = "multipart/form-data";
            public static readonly string Byteranges = "multipart/byteranges";
        }

        public static class Text
        {
            public static readonly string Plain = "text/plain";
            public static readonly string Html = "text/plain";
            public static readonly string Css = "text/css";
            public static readonly string Xml = "text/xml";
            public static readonly string XHtml = "text/xhtml+xml";
        }

        public static class Video
        {
            public static readonly string WebM = "video/webm";
            public static readonly string Ogg = "video/ogg";
        }

    }
}
