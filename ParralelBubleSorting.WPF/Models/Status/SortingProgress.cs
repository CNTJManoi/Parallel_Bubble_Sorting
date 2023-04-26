using ParralelBubleSorting.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParralelBubleSorting.WPF.Models.Status
{
    internal class SortingProgress : ISortingProgress
    {
        public SortingProgress()
        {
            StatusOneTask = "Готов";
            StatusTwoTask = "Готов";
            StatusThreeTask = "Готов";
            
        }

        public string StatusOneTask { get; set; }
        public string StatusTwoTask { get; set; }
        public string StatusThreeTask { get; set; }

        public void UpdateStatus(Task taskOne, Task taskTwo, Task taskThree)
        {
            StatusOneTask = GenerateStringStatus(taskOne);
            StatusTwoTask = GenerateStringStatus(taskTwo);
            StatusThreeTask = GenerateStringStatus(taskThree);
        }
        private string GenerateStringStatus(Task task)
        {
            switch (task.Status)
            {
                case TaskStatus.Created:
                    return "Создан";
                    break;
                case TaskStatus.WaitingForActivation:
                    return "Ждет реализации";
                    break;
                case TaskStatus.WaitingToRun:
                    return "Ждет запуска";
                    break;
                case TaskStatus.Running:
                    return "Запущен";
                    break;
                case TaskStatus.WaitingForChildrenToComplete:
                    return "Ждет выполнение других";
                    break;
                case TaskStatus.RanToCompletion:
                    return "Успешно выполнен";
                    break;
                case TaskStatus.Canceled:
                    return "Отменен";
                    break;
                case TaskStatus.Faulted:
                    return "Ошибка";
                    break;
            }
            return "Ошибка";
        }
    }
}
