using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;

namespace TrainerEvaluate.Utility
{
    public class ZipHelper
    {
  
        /// <summary>
        /// 功能：压缩文件（暂时只压缩文件夹下一级目录中的文件，文件夹及其子级被忽略）
        /// </summary>
        /// <param name="dirPath">被压缩的文件夹夹路径</param>
        /// <param name="zipFilePath">生成压缩文件的路径，为空则默认与被压缩文件夹同一级目录，名称为：文件夹名+.zip</param>
        /// <param name="err">出错信息</param>
        /// <returns>是否压缩成功</returns>
        public static bool ZipFile(string dirPath, string zipFilePath, out string err)
        {
            err = "";
            if (dirPath == string.Empty)
            {
                err = "要压缩的文件夹不能为空！";
                return false;
            }
            if (!Directory.Exists(dirPath))
            {
                err = "要压缩的文件夹不存在！";
                return false;
            }
            //压缩文件名为空时使用文件夹名＋.zip
            if (zipFilePath == string.Empty)
            {
                if (dirPath.EndsWith("\\"))
                {
                    dirPath = dirPath.Substring(0, dirPath.Length - 1);
                }
                zipFilePath = dirPath + ".zip";
            }

            try
            {
                string[] filenames = Directory.GetFiles(dirPath);
                using (ZipOutputStream s = new ZipOutputStream(File.Create(zipFilePath)))
                {
                    s.SetLevel(9);
                    byte[] buffer = new byte[4096];
                    foreach (string file in filenames)
                    {
                        ZipEntry entry = new ZipEntry(Path.GetFileName(file));
                        entry.DateTime = DateTime.Now;
                        s.PutNextEntry(entry);
                        using (FileStream fs = File.OpenRead(file))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }
                    s.Finish();
                    s.Close();
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dirPathLs"></param>
        /// <param name="zipFilePath"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public static bool ZipFile(List<string[]> filenames, string zipFilePath, out string err)
        {
            err = "";
            if (filenames == null || filenames.Count == 0)
            {
                err = "要压缩的文件不能为空！";
                return false;
            }
            //压缩文件名为空时使用文件夹名＋.zip
            //if (zipFilePath == string.Empty)
            //{
            //    if (dirPath.EndsWith("\\"))
            //    {
            //        dirPath = dirPath.Substring(0, dirPath.Length - 1);
            //    }
            //    zipFilePath = dirPath + ".zip";
            //}

            try
            {
                //string[] filenames = Directory.GetFiles(dirPath);
                using (ZipOutputStream s = new ZipOutputStream(File.Create(zipFilePath)))
                {
                    s.SetLevel(9);
                    byte[] buffer = new byte[4096];
                    foreach (string[] file in filenames)
                    {
                        ZipEntry entry = new ZipEntry(file[1]);
                        entry.DateTime = DateTime.Now;
                        s.PutNextEntry(entry);
                        using (FileStream fs = File.OpenRead(file[0]))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }
                    s.Finish();
                    s.Close();
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
            return true;
        }


    }
}
