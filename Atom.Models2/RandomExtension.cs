using System;
using System.Collections.Generic;
using System.Text;

namespace Atom.Models2
{
    public static class RandomExtension
    {
        /// <summary>
        /// Длительность работ от 0 до 30 дней.
        /// Минимальная длительность работ от 0 до 30 дней
        /// Название 5 букв от guid.
        /// Цена сдвига работы назад от -1 до 50
        /// Цена сдвига работы вперед от -1 до 50
        /// Цена изменения длительности работы от -1 до 50
        /// Начало работы с 2020 01 01 и до 2020 12 28
        /// </summary>
        /// <param name="random"></param>
        /// <param name="countgroup"></param>
        /// <param name="countstage"></param>
        /// <param name="countwork"></param>
        /// <returns></returns>
        public static Project NextProject(this Random random, int countgroup, int countstage, int countwork)
        {
            Project proj = new Project();
            for(var i =0; i< countgroup; i++)
            {
                var gr = new Group();
                for (var j = 0; j < countwork; j++)
                {
                    var day = random.Next(1, 28);
                    var month = random.Next(1, 12);
                    var w = new Work()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Start = new DateTime(2020, month, day),
                        Duration = random.Next(0, 30),                     
                        Name = Guid.NewGuid().ToString().Substring(0,5)+"...",
                        PriceEarlier = random.Next(-1, 50),
                        PriceDurationChanged = random.Next(-1, 50),
                        PriceLate = random.Next(-1, 50)
                    };                
                    w.DurationMin = random.Next(0, (int)w.Duration);               
                    gr.Works.Add(w);
                }
                proj.Groups.Add(gr);
            }
            for (var i = 0; i < countstage; i++)
            {
                var day = random.Next(1, 28);
                var month = random.Next(1, 12);
                var gr = new Stage()
                {
                    Id = Guid.NewGuid().ToString(),
                    Start = new DateTime(2020, month, day),
                    Duration = random.Next(30, 90),
                    Name = Guid.NewGuid().ToString().Substring(5),
                    PriceEarlier = random.Next(10000, 100000),
                    PriceDurationChanged = random.Next(10000, 100000),
                    PriceLate = random.Next(10000, 100000)
                };
                gr.DurationMin = random.Next(0, (int)gr.Duration);
                proj.Stages.Add(gr);
            }
            return proj;
        }


      


       
    }
}
