using System;
using System.IO;
using cope.Extensions;

namespace cope.DawnOfWar2
{
    /// <summary>
    /// Class for files in general.
    /// </summary>
    public class UniFile : Taggable, IFile
    {
        /// <summary>
        /// Path to the file.
        /// </summary>
        protected string m_filePath = "no path set";

        #region constructors

        /// <summary>
        /// Constructs a new UniFile and reads it's data from the specified Byte[].
        /// </summary>
        /// <param name="data">Byte[] to read from.</param>
        public UniFile(Byte[] data)
        {
            Stream = new MemoryStream(data, true);
        }

        /// <summary>
        /// Constructs a new UniFile and reads it's data from the specified file.
        /// </summary>
        /// <param name="filePath">Path of the file to read from.</param>
        /// <param name="createIfNotFound">If true, tries to create the specified file if it is not found.</param>
        public UniFile(string filePath, bool createIfNotFound = true)
            : this(filePath, FileAccess.ReadWrite, FileShare.None, createIfNotFound)
        {
        }

        /// <summary>
        /// Constructs a new UniFile and reads it's data from the specified file.
        /// </summary>
        /// <param name="filePath">Path of the file to read from.</param>
        /// <param name="access"></param>
        /// <param name="share"></param>
        /// <param name="createIfNotFound">If true, tries to create the specified file if it is not found.</param>
        public UniFile(string filePath, FileAccess access, FileShare share, bool createIfNotFound = true)
        {
            m_filePath = Path.GetFullPath(filePath);
            if (!File.Exists(m_filePath))
            {
                if (createIfNotFound)
                {
                    try
                    {
                        Stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, share);
                    }
                    catch(Exception ex)
                    {
                        Close();
                        throw new CopeException(ex, "Error while trying to create " + m_filePath + "'. See inner exception.");
                    }
                }
                return;
            }
            try
            {
                Stream = new FileStream(filePath, FileMode.Open, access, share);
            }
            catch (Exception ex)
            {
                Close();
                throw new CopeException(ex, "Error while trying to open " + m_filePath + "'. See inner exception.");
            }
        }

        /// <summary>
        /// Constructs a new UniFile using the provided stream.
        /// </summary>
        /// <param name="stream">The stream to read the data for the UniFile from.</param>
        public UniFile(Stream stream)
        {
            Stream = stream;
        }

        /// <summary>
        /// Constructs a new UniFile.
        /// </summary>
        public UniFile()
        {
        }

        public UniFile(UniFile file)
        {
            m_filePath = file.m_filePath;
            Stream = file.Stream;
            Tag = file.Tag;
        }

        #endregion constructors

        #region methods

        /// <summary>
        /// Writes data to the current stream.
        /// </summary>
        public void WriteData()
        {
            if (Stream == null)
            {
                if (string.IsNullOrEmpty(FilePath))
                    throw new CopeException(
                        "UniFile - error executing WriteData: No stream found and no FilePath available!");
                Stream = new FileStream(FilePath, FileMode.OpenOrCreate);
            }
            Write(Stream);
        }

        /// <summary>
        /// Writes the data of this File to a location of your choice. May throw exceptions, does not alter the Stream-property.
        /// </summary>
        /// <param name="path">The path to write the file to, e.g. "C:/my_file.txt".</param>
        public void WriteDataTo(string path)
        {
            if (!Directory.Exists(path.SubstringBeforeLast('\\', true)))
                Directory.CreateDirectory(path.SubstringBeforeLast('\\', true));
            FileStream stream = null;
            try
            {
                stream = new FileStream(path, FileMode.Create);
                Write(stream);
            }
            catch (Exception ex)
            {
                throw new CopeException(ex, "Error while writing UniFile to " + path + ". See inner exception");
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }

        /// <summary>
        /// Writes the data to the specified stream. It will NOT close the stream.
        /// </summary>
        /// <param name="stream"></param>
        protected virtual void Write(Stream stream)
        {
        }

        /// <summary>
        /// Reads the data from the current stream.
        /// </summary>
        /// <exception cref="CopeException">Error while trying to read data in UniFile! See inner exception.</exception>
        public void ReadData()
        {
            if (Stream == null)
                return;
            try
            {
                Read(Stream);
            }
            catch (Exception ex)
            {
                throw new CopeException(ex, "Error while trying to read data in UniFile! See inner exception.");
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// Reads the data from the specified stream.
        /// </summary>
        /// <param name="stream"></param>
        protected virtual void Read(Stream stream)
        {
        }

        /// <summary>
        /// Closes the stream.
        /// </summary>
        public void Close()
        {
            if (Stream == null)
                return;
            Stream.Close();
            Stream = null;
        }

        /// <summary>
        /// Reads the entire stream and returns a byte-array with the Stream's content.
        /// </summary>
        /// <returns></returns>
        public byte[] ConsumeStream()
        {
            if (Stream == null)
                return null;
            byte[] memory;
            try
            {
                int length = (int)(Stream.Length - Stream.Position);
                memory = new byte[length];
                Stream.Read(memory, 0, length);
                Stream.Close();
            }
            catch (Exception ex)
            {
                return null;
            }
            return memory;
        }

        public override string ToString()
        {
            return m_filePath;
        }

        #endregion methods

        #region Properties

        /// <summary>
        /// Gets or sets the stream of this UniFile.
        /// </summary>
        public virtual Stream Stream { get; set; }

        /// <summary>
        /// Gets or sets the filename with extension.
        /// </summary>
        public string FileName
        {
            get
            {
                return Path.GetFileName(m_filePath);
            }
            set
            {
                if (m_filePath.Contains('\\'))
                    m_filePath = m_filePath.SubstringBeforeLast('\\', true) + value;
                else
                    m_filePath = value;
            }
        }

        /// <summary>
        /// Gets or sets the extension of the file.
        /// </summary>
        public string FileExtension
        {
            get { return m_filePath.SubstringAfterLast('.'); }
            set { m_filePath = m_filePath.SubstringBeforeLast('.', true) + value; }
        }

        /// <summary>
        /// Gets or sets the path to the file.
        /// </summary>
        public string FilePath
        {
            get { return m_filePath; }
            set { m_filePath = value.Replace('/', '\\'); }
        }

        #endregion Properties

        #region IFile Member

        ///<summary>
        ///</summary>
        public void Delete()
        {
            Close();
            if (File.Exists(m_filePath))
                File.Delete(m_filePath);
        }

        ///<summary>
        ///</summary>
        ///<param name="path"></param>
        public void Copy(string path)
        {
            if (File.Exists(m_filePath))
                File.Copy(m_filePath, path);
            else
                throw new CopeException("Can't copy file " + m_filePath + " as it does not exist at all!");
        }
    
        ///<summary>
        ///</summary>
        ///<param name="path"></param>
        public void Move(string path)
        {
            Close();
            if (File.Exists(m_filePath))
            {
                File.Move(m_filePath, path);
                Stream = File.Open(path, FileMode.OpenOrCreate);
            }
            else
                throw new CopeException("Can't move file " + m_filePath + " as it does not exist at all!");
        }

        #endregion IFile Member
    }

    public interface IFile
    {
        void Delete();
        void Copy(string path);
        void Move(string path);
    }
}