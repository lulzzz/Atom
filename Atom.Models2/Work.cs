using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Atom.Models2
{
    [Serializable]
    public class Work : ICloneable
    {
       
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public long Duration { get; set; }

        /// <summary>
        /// 0 бесплатный сдви
        /// n стоимость в рублях
        /// </summary>
        public long DurationMin { get; set; }

        #region Стоимость

        /// <summary>
        /// -1 запрет
        /// 0 бесплатный сдви
        /// n стоимость в рублях
        /// </summary>
        public long PriceEarlier { get; set; }
        public long PriceLate { get; set; }

        /// <summary>
        /// -1 запрет
        /// 0 бесплатный сдви
        /// n стоимость в рублях
        /// </summary>
        public long PriceDurationChanged { get; set; }

        public object Clone()
        {
            return new Work()
            {
                Id = this.Id,
                Name = this.Name,
                Duration = this.Duration,
                DurationMin = this.DurationMin,
                Finish = this.Finish,
                PriceDurationChanged = this.PriceDurationChanged,
                PriceEarlier = this.PriceEarlier,
                PriceLate = this.PriceLate,
                Start = this.Start   
            };
        }
        #endregion
    }
}
