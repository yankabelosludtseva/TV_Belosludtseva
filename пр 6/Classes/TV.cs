using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace пр_6.Classes
{
    public class TV
    {
        private int activeChannel;
        private int activeVolume;

        public int ActiveChannel
        {
            get
            {
                return activeChannel;
            }
            set
            {
                // Если значение выходит за верхнюю границу
                if (value >= Channels.Count)
                {
                    activeChannel = 0;
                }
                // Если значение выходит за нижнюю границу
                else if (value < 0)
                {
                    activeChannel = Channels.Count - 1;
                }
                // Если значение в допустимом диапазоне
                else
                {
                    activeChannel = value;
                }
            }
        }

        public List<Channel> Channels = new List<Channel>();

        public TV()
        {
            // Это что-то вроде репозитория в котором заносятся данные
            Channels.Add(new Channel()
            {
                Name = "Практическое занятие №3 Объявление классов и создание объектов в С#.mp4",
                Src = @"C:\Users\aooshchepkov\Downloads\1.mp4"
            });
            Channels.Add(new Channel()
            {
                Name = "Практическое занятие №4 Использование методов в ООП Разница между простыми и статическими методами",
                Src = @"C:\Users\aooshchepkov\Downloads\2.mp4"
            });
            Channels.Add(new Channel()
            {
                Name = "Практическое задание №5 Конструкторы в ООП.mp4",
                Src = @"C:\Users\aooshchepkov\Downloads\3.mp4"
            });
        }

        public void ChangeChannel(MediaElement _MediaElement, TextBlock _NameChannel)
        {
            // Реализация метода смены канала
            if (Channels.Count > 0 && ActiveChannel >= 0 && ActiveChannel < Channels.Count)
            {
                var channel = Channels[ActiveChannel];
                _MediaElement.Source = new Uri(channel.Src);
                _NameChannel.Text = channel.Name;
            }
        }

        public void SwapChannel(MediaElement _MediaElement, Label _NameChannel)
        {
            // Анимация старта (уход в 0 по прозрачности)
            DoubleAnimation startAnimation = new DoubleAnimation();
            // Начальное значение анимации
            startAnimation.From = 1;
            // Конечное значение анимации
            startAnimation.To = 0;
            // Продолжительность анимации
            startAnimation.Duration = TimeSpan.FromSeconds(0.6);

            // После выполнения анимации выполняем следующий код
            startAnimation.Completed += delegate
            {
                // Изменяем ресурс канала
                _MediaElement.Source = new Uri(this.Channels[this.ActiveChannel].Src);
                // Воспроизводим видео
                _MediaElement.Play();

                // Анимация конца (уход в 1 по прозрачности)
                DoubleAnimation endAnimation = new DoubleAnimation();
                // Начальное значение анимации
                endAnimation.From = 0;
                // Конечное значение анимации
                endAnimation.To = 1;
                // Продолжительность анимации
                endAnimation.Duration = TimeSpan.FromSeconds(0.6);

                // Запускаем анимацию прозрачности
                _MediaElement.BeginAnimation(MediaElement.OpacityProperty, endAnimation);
            };

            // Запускаем анимацию прозрачности
            _MediaElement.BeginAnimation(MediaElement.OpacityProperty, startAnimation);

            // Изменяем наименование канала
            _NameChannel.Content = this.Channels[this.ActiveChannel].Name;
        }

        public void NextChannel(MediaElement _MediaElement, Label _NameChannel)
        {
            // Увеличиваем канал
            ActiveChannel++;
            // Заменяем видеоролик
            SwapChannel(_MediaElement, _NameChannel);
        }

        public void BackChannel(MediaElement _MediaElement, Label _NameChannel)
        {
            // Уменьшаем канал
            ActiveChannel--;
            // Заменяем видеоролик
            SwapChannel(_MediaElement, _NameChannel);
        }

        public void UpSound()
        {
            
        }

        public void DownSound()
        {
           
        }
    }
}
