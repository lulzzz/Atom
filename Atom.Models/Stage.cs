using System;
using System.Collections.Generic;
using System.Text;

namespace Atom.Models
{
    /// <summary>
    /// Веха – специфическая работа нулевой длительности. Как правило, в плане проекта не более 100-150 вех. 
    /// Как правило, стоимость сдвига даты старта вехи <> 0. 
    /// Как правило, стоимость сдвига старта вехи значительно превышает стоимость сдвига работы и составляет значимую (5% и более) 
    /// от общей стоимости проекта. Значение атрибутов работы, выделяющих веху среди других работ:
    /// </summary>
    public class Stage
    {
        public string Id { get; set; }

        /// <summary>
        /// Название веха. 
        /// Пример: Мониторинг, анализ, проектирование
        /// </summary>
        public string Name { get; set; }
        public DateTime DateStart { get; set; }

        /// <summary>
        /// -1 запрет.
        /// 0 бесплатный сдвиг.
        /// Стоимость сдвига
        /// </summary>
        public long PriceEarlier { get; set; }

        /// <summary>
        /// -1 запрет.
        /// 0 бесплатный сдвиг.
        /// Стоимость сдвига
        /// </summary>
        public long PriceLater { get; set; }

        /// <summary>
        /// Длительность.
        /// </summary>
        public TimeSpan Duration { get; set; }

        public TimeSpan DurationMin { get; set; }

        /// <summary>
        /// -1 запрет.
        /// 0 бесплатный сдвиг.
        /// Стоимость сдвига
        /// </summary>
        public long PriceDuration { get; set; }
    }
}
