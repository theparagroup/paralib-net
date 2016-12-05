using System;
using System.IO;
using Android.Print;
using Android.Content;
using Android.OS;
using Android.Runtime;

namespace com.paralib.Xandroid.Utils
{
    public class PDFPrinter
    {
        public static void Print(Context context, string jobName, string documentName, byte[] pdf, int pageCount)
        {
            var printMgr = (PrintManager)context.GetSystemService(Context.PrintService);
            var atts = new PrintAttributes.Builder().SetMinMargins(PrintAttributes.Margins.NoMargins).Build();

            printMgr.Print("Razor HMTL Hybrid", new PDFPrintAdapter(documentName, pdf, pageCount), atts);
        }


        public class PDFPrintAdapter : PrintDocumentAdapter
        {
            protected string _documentName;
            protected byte[] _pdf;
            protected int _pageCount;

            public PDFPrintAdapter(string documentName, byte[] pdf, int pageCount)
            {
                _documentName = documentName;
                _pdf = pdf;
                _pageCount = pageCount;
            }

            public override void OnLayout(PrintAttributes oldAttributes, PrintAttributes newAttributes, CancellationSignal cancellationSignal, LayoutResultCallback callback, Bundle extras)
            {
                var printInfo = new PrintDocumentInfo
                .Builder(_documentName)
                .SetContentType(PrintContentType.Document)
                .SetPageCount(_pageCount)
                .Build();

                callback.OnLayoutFinished(printInfo, true);
            }

            public override void OnWrite(PageRange[] pages, ParcelFileDescriptor destination, CancellationSignal cancellationSignal, WriteResultCallback callback)
            {

                var javaStream = new Java.IO.FileOutputStream(destination.FileDescriptor);
                var osi = new OutputStreamInvoker(javaStream);

                osi.Write(_pdf, 0, _pdf.Length);

                callback.OnWriteFinished(pages);
            }
        }

    }
}
