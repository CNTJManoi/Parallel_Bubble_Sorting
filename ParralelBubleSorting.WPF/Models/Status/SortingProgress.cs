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
            if(taskOne != null) StatusOneTask = GenerateStringStatus(taskOne);
            if(taskTwo != null) StatusTwoTask = GenerateStringStatus(taskTwo);
            if(taskThree != null) StatusThreeTask = GenerateStringStatus(taskThree);
        }
        private string GenerateStringStatus(Task task)
        {
            switch (task.Status)
            {
                case TaskStatus.Created:
                    return "Создан";
                case TaskStatus.WaitingForActivation:
                    return "Ждет реализации";
                case TaskStatus.WaitingToRun:
                    return "Ждет запуска";
                case TaskStatus.Running:
                    return "Запущен";
                case TaskStatus.WaitingForChildrenToComplete:
                    return "Ждет выполнение других";
                case TaskStatus.RanToCompletion:
                    return "Успешно выполнен";
                case TaskStatus.Canceled:
                    return "Отменен";
                case TaskStatus.Faulted:
                    return "Ошибка";
            }
            return "Ошибка";
        }
    }
}
