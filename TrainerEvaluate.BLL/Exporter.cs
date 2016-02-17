using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Svg;
using Svg.Transforms;
using Image = iTextSharp.text.Image;
using Rectangle = iTextSharp.text.Rectangle;

namespace TrainerEvaluate.BLL
{
    public class Exporter
    {
        /// <summary>
        /// Default file name to use for chart exports if not otherwise specified.
        /// </summary>
        private const string DefaultFileName = "Chart";

        /// <summary>
        /// PDF metadata Creator string.
        /// </summary>
        private const string PdfMetaCreator =
          "Exporting Module for Highcharts JS from";

        /// <summary>
        /// Gets the HTTP Content-Disposition header to be sent with an HTTP
        /// response that will cause the client's browser to open a file save
        /// dialog with the proper file name.
        /// </summary>
        public string ContentDisposition { get; private set; }

        /// <summary>
        /// Gets the MIME type of the exported output.
        /// </summary>
        public string ContentType { get; private set; }

        /// <summary>
        /// Gets the file name with extension to use for the exported chart.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Gets the chart name (same as file name without extension).
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the SVG chart document (XML text).
        /// </summary>
        public string Svg { get; private set; }

        /// <summary>
        /// Gets the pixel width of the exported chart image.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Initializes a new chart Export object using the specified file name, 
        /// output type, chart width and SVG text data.
        /// </summary>
        /// <param name="fileName">The file name (without extension) to be used 
        /// for the exported chart.</param>
        /// <param name="type">The requested MIME type to be generated. Can be
        /// 'image/jpeg', 'image/png', 'application/pdf' or 'image/svg+xml'.</param>
        /// <param name="width">The pixel width of the exported chart image.</param>
        /// <param name="svg">An SVG chart document to export (XML text).</param>
        public Exporter(
          string fileName,
          string type,
          int width,
          string svg)
        {
            string extension;

            this.ContentType = type.ToLower();
            this.Name = fileName;
            this.Svg = svg;
            this.Width = width;

            // Validate requested MIME type.
            switch (ContentType)
            {
                case "image/jpeg":
                    extension = "jpg";
                    break;

                case "image/png":
                    extension = "png";
                    break;

                case "application/pdf":
                    extension = "pdf";
                    break;

                case "application/x-excel":
                    extension = "xls";
                    break;

                case "image/svg+xml":
                    extension = "svg";
                    break;

                // Unknown type specified. Throw exception.
                default:
                    throw new ArgumentException(
                      string.Format("Invalid type specified: '{0}'.", type));
            }

            // Determine output file name.
            this.FileName = string.Format(
              "{0}.{1}",
              string.IsNullOrEmpty(fileName) ? DefaultFileName : fileName,
              extension);

            // Create HTTP Content-Disposition header.
            this.ContentDisposition =
              string.Format("attachment; filename={0}", this.FileName);
        }

        /// <summary>
        /// Creates an SvgDocument from the SVG text string.
        /// </summary>
        /// <returns>An SvgDocument object.</returns>
        private SvgDocument CreateSvgDocument()
        {
            SvgDocument svgDoc;

            // Create a MemoryStream from SVG string.
            using (MemoryStream streamSvg = new MemoryStream(
              Encoding.UTF8.GetBytes(this.Svg)))
            {
                // Create and return SvgDocument from stream.
                svgDoc = SvgDocument.Open(streamSvg);
            }

            // Scale SVG document to requested width.
            svgDoc.Transforms = new SvgTransformCollection();
            float scalar = (float)this.Width / (float)svgDoc.Width;
            scalar = 1.5f;
            svgDoc.Transforms.Add(new SvgScale(scalar, scalar));
            svgDoc.Width = new SvgUnit(svgDoc.Width.Type, svgDoc.Width * scalar);
            svgDoc.Height = new SvgUnit(svgDoc.Height.Type, svgDoc.Height * scalar);

            return svgDoc;
        }



        //private SvgDocument CreateSvgDocument()
        //{
        //    SvgDocument svgDoc;

        //    XmlDocument xml = new XmlDocument();
        //    xml.LoadXml(this.Svg);
        //    XmlNodeList nodeListAllg = xml.GetElementsByTagName("g");
        //    Dictionary<int, XmlNode[,]> dic = new Dictionary<int, XmlNode[,]>();
        //    int i = 0;
        //    foreach (XmlNode xNod in nodeListAllg)
        //    {
        //        i++;
        //        XmlNode xmlvisibility = xNod.Attributes.GetNamedItem("class");
        //        if (xmlvisibility != null && xmlvisibility.Value == "highcharts-series-group")
        //        {
        //            foreach (XmlNode xNod2 in xNod.ChildNodes)
        //            {
        //                i++;
        //                XmlNode xmlvisibility1 = xNod2.Attributes.GetNamedItem("visibility");
        //                if (xmlvisibility1 != null && xmlvisibility1.Value == "hidden")
        //                {
        //                    XmlNode[,] xmln = new XmlNode[1, 2];
        //                    xmln[0, 0] = xNod;
        //                    xmln[0, 1] = xNod2;
        //                    dic.Add(i, xmln);
        //                }
        //            }
        //        }
        //        else if (xmlvisibility != null && xmlvisibility.Value == "highcharts-tooltip")
        //        {
        //            XmlNode[,] xmln = new XmlNode[1, 2];
        //            xmln[0, 0] = xml.FirstChild;
        //            xmln[0, 1] = xNod;
        //            dic.Add(i, xmln);
        //        }
        //    }
        //    foreach (KeyValuePair<int, XmlNode[,]> a in dic)
        //    {
        //        a.Value[0, 0].RemoveChild(a.Value[0, 1]);
        //    }

        //    this.Svg = xml.OuterXml;

        //    // Create a MemoryStream from SVG string.
        //    using (MemoryStream streamSvg = new MemoryStream(
        //      Encoding.UTF8.GetBytes(this.Svg)))
        //    {
        //        // Create and return SvgDocument from stream.
        //        svgDoc = SvgDocument.Open(streamSvg);
        //    }

        //    // Scale SVG document to requested width.
        //    svgDoc.Transforms = new SvgTransformCollection();
        //    float scalar = (float)this.Width / (float)svgDoc.Width;
        //    svgDoc.Transforms.Add(new SvgScale(scalar, scalar));
        //    svgDoc.Width = new SvgUnit(svgDoc.Width.Type, svgDoc.Width * scalar);
        //    svgDoc.Height = new SvgUnit(svgDoc.Height.Type, svgDoc.Height * scalar);

        //    return svgDoc;
        //}




        /// <summary>
        /// Exports the chart to the specified HttpResponse object. This method
        /// is preferred over WriteToStream() because it handles clearing the
        /// output stream and setting the HTTP reponse headers.
        /// </summary>
        /// <param name="httpResponse"></param>
        public void WriteToHttpResponse(HttpResponse httpResponse)
        {
            httpResponse.ClearContent();
            httpResponse.ClearHeaders();
            httpResponse.ContentType = this.ContentType;
            httpResponse.AddHeader("Content-Disposition", this.ContentDisposition);
            WriteToStream(httpResponse.OutputStream);
        }

        /// <summary>
        /// Exports the chart to the specified output stream as binary. When 
        /// exporting to a web response the WriteToHttpResponse() method is likely
        /// preferred.
        /// </summary>
        /// <param name="outputStream">An output stream.</param>
        internal void WriteToStream(Stream outputStream)
        {
            switch (this.ContentType)
            {
                case "application/x-excel":
                case "image/jpeg":
                    CreateSvgDocument().Draw().Save(
                      outputStream,
                      ImageFormat.Jpeg);
                    break;
                //case "application/x-excel":
                //    CreateSvgDocument().Draw().Save(
                //      outputStream,
                //      ImageFormat.Jpeg);
                //    break;
                case "image/png":
                    // PNG output requires a seekable stream.
                    using (MemoryStream seekableStream = new MemoryStream())
                    {
                        CreateSvgDocument().Draw().Save(
                            seekableStream,
                            ImageFormat.Png);
                        seekableStream.WriteTo(outputStream);
                    }
                    break;

                case "application/pdf":
                    SvgDocument svgDoc = CreateSvgDocument();

                    // Create PDF document.
                    using (Document pdfDoc = new Document())
                    {
                        // Scalar to convert from 72 dpi to 150 dpi.
                        float dpiScalar = 150f / 72f;

                        // Set page size. Page dimensions are in 1/72nds of an inch.
                        // Page dimensions are scaled to boost dpi and keep page
                        // dimensions to a smaller size.
                        pdfDoc.SetPageSize(new Rectangle(
                          svgDoc.Width / dpiScalar,
                          svgDoc.Height / dpiScalar));

                        // Set margin to none.
                        pdfDoc.SetMargins(0f, 0f, 0f, 0f);

                        // Create PDF writer to write to response stream.
                        using (PdfWriter pdfWriter = PdfWriter.GetInstance(
                          pdfDoc,
                          outputStream))
                        {
                            // Configure PdfWriter.
                            pdfWriter.SetPdfVersion(PdfWriter.PDF_VERSION_1_5);
                            pdfWriter.CompressionLevel = PdfStream.DEFAULT_COMPRESSION;

                            // Add meta data.
                            pdfDoc.AddCreator(PdfMetaCreator);
                            pdfDoc.AddTitle(this.Name);

                            // Output PDF document.
                            pdfDoc.Open();
                            pdfDoc.NewPage();

                            // Create image element from SVG image.
                            Image image = Image.GetInstance(svgDoc.Draw(), ImageFormat.Bmp);
                            image.CompressionLevel = PdfStream.DEFAULT_COMPRESSION;

                            // Must match scaling performed when setting page size.
                            image.ScalePercent(100f / dpiScalar);

                            // Add image element to PDF document.
                            pdfDoc.Add(image);

                            pdfDoc.CloseDocument();
                        }
                    }

                    break;

                case "image/svg+xml":
                    using (StreamWriter writer = new StreamWriter(outputStream))
                    {
                        writer.Write(this.Svg);
                        writer.Flush();
                    }

                    break;

                default:
                    throw new InvalidOperationException(string.Format(
                      "ContentType '{0}' is invalid.", this.ContentType));
            }

            outputStream.Flush();
        }

         

        public byte[] GetReportImg(out int height,out int width)
        { 
            //using (MemoryStream seekableStream = new MemoryStream())
            //{
            //    CreateSvgDocument().Draw().Save(
            //        seekableStream,
            //        ImageFormat.Png);
            //  //  seekableStream.WriteTo(outputStream); 
               

            //    byte[] bytes = new byte[seekableStream.Length];
            //    seekableStream.Read(bytes, 0, bytes.Length);
            //    // 设置当前流的位置为流的开始
            //    seekableStream.Seek(0, SeekOrigin.Begin);
            //    return bytes;   
            //}

            var svgDoc = CreateSvgDocument(); 
            var bm =svgDoc.Draw();
            width= bm.Width;
            height = bm.Height;
            MemoryStream ms = new MemoryStream();
            bm.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] bytes = ms.GetBuffer();  //byte[]   bytes=   ms.ToArray(); 这两句都可以，至于区别么，下面有解释
            ms.Close(); 
            return bytes; 
        }









    }  
}
