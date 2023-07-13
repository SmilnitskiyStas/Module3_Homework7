using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncLogger.Enums;

namespace AsyncLogger.Models
{
    internal class Logger
    {
        public event Action<EnumStateWriteToFile> CountsByWriteLogs;

        private static Logger instance;

        private string[] _dataLog = new string[200];

        private string[] _dataLogForWriteToFile = null;

        private int _count = 0;

        private Logger()
        {
        }

        public static Logger Instance()
        {
            if (instance == null)
            {
                instance = new Logger();
            }

            return instance;
        }

        /// <summary>
        /// Формування масиву із Логами, ти виводимо на консоль.
        /// </summary>
        /// <param name="enumLogStatus">Тип логу.</param>
        /// <param name="message">Повідомлення помилки.</param>
        public void WriteLog(EnumLogStatus enumLogStatus, string message)
        {
            string logMessage = $"{DateTime.Now}: {enumLogStatus}: {message}";

            Console.WriteLine(logMessage);

            _dataLog[_count++] = logMessage;

            if (CountsByWriteLogs != null)
            {
                CountsByWriteLogs(GetCountLogsByWriteForLogger(_count));
            }
        }

        /// <summary>
        /// Повернення масиву із списком логів.
        /// </summary>
        /// <returns>Строковий масив.</returns>
        public string[] GetDataLog()
        {
            _dataLogForWriteToFile = _dataLog;

            Array.Resize(ref _dataLogForWriteToFile, _count);

            return _dataLogForWriteToFile;
        }

        private EnumStateWriteToFile GetCountLogsByWriteForLogger(int count)
        {
            Console.WriteLine(count);

            if (count % 20 == 0 || count == 50)
            {
                Thread.Sleep(3000);
                return EnumStateWriteToFile.Write;
            }
            else
            {
                return EnumStateWriteToFile.Work;
            }
        }
    }
}
