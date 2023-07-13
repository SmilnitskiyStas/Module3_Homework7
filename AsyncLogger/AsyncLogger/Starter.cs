using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncLogger.DataWriter;
using AsyncLogger.Enums;
using AsyncLogger.Exceptions;
using AsyncLogger.Models;

namespace AsyncLogger
{
    internal class Starter
    {
        private string _fileName;

        public Starter()
        {
        }

        public async void Run()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(RunLogger());
            tasks.Add(RunLogger());

            await Task.WhenAll(tasks);
        }

        private async Task RunLogger()
        {
            Random random = new Random();

            Actions actions = new Actions();

            Result result = null;

            Logger.Instance().CountsByWriteLogs += CountsByWriteLogs;

            for (int i = 0; i < 50; i++)
            {
                try
                {
                    switch (random.Next(0, 3))
                    {
                        case 0:
                            result = actions.InfoMethod();
                            break;
                        case 1:
                            actions.WarningMethod();
                            break;
                        case 2:
                            actions.ErrorMethod();
                            break;
                    }
                }
                catch (BusinessException businessException)
                {
                    Console.WriteLine(businessException.Message);

                    Logger.Instance().WriteLog(EnumLogStatus.Warning, $"Action got this custom Exception: {businessException.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message, ex.Source);

                    Logger.Instance().WriteLog(EnumLogStatus.Error, $"Action failed by reason: {ex.Source}");
                }
            }

            await Task.Delay(1000);
        }

        private async void CountsByWriteLogs(EnumStateWriteToFile status)
        {
            Console.WriteLine(status);

            if (status == EnumStateWriteToFile.Write)
            {
                _fileName = $"{DateTime.Now.ToString("hh.mm.ss dd.MM.yyyy")}.txt";

                FileService fileService = new FileService(_fileName);

                await Task.Run(() => fileService.WriteLogInTheFileAsync());
            }
        }
    }
}
