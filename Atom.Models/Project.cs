using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Atom.Models
{
    public class Project
    {
        #region Политика генерирование Uid
        public static string GetUid(Account account, string name = "")
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(account.Login);
            stringBuilder.Append(name);
            stringBuilder.Append(DateTime.Now.Ticks);
            return stringBuilder.ToString();
        }
        #endregion

        #region Base
        public string Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public string ParentId { get => (Parent != null) ? Parent.Id : string.Empty; }

        public Project()
        {

        }
        public Project(string id) : this()
        {
            Id = id;
        }
        #endregion

        #region Пользователи и контроль

        [JsonIgnore]
        public List<Account> ProjectOwners { get; set; } = new List<Account>();

        [JsonIgnore]
        public List<Account> ProjectListener { get; set; } = new List<Account>();

        [JsonIgnore]
        public List<Account> ProjectExecutors { get; set; } = new List<Account>();

        [JsonIgnore]
        public List<Account> ProjectViewers { get; set; } = new List<Account>();
        #endregion

        #region Подпроекты
        [JsonIgnore]
        public Project Parent { get; set; }

        [JsonIgnore]
        public bool IsPure { get; set; }

        [JsonProperty]
        public Dictionary<string, Project> SubProjects { get; set; } = new Dictionary<string, Project>();
        [JsonProperty]
        public List<Link> Links { get; set; } = new List<Link>();

        public void AddChild(Project project)
        {
            SubProjects.Add(project.Id, project);
            Links.Add(new Link(this, project, LinkType.Parent));
        }
        public void AddLink(Link link)
        {
            Links.Add(link);           
        }

        public LinkMatrix GetLinkMatrix()
        {
           
            return new LinkMatrix(this); 

        }
        #endregion

        #region Ресурсы
        [JsonIgnore]
        public ResourceStorage ExistingResource { get; set; } = new ResourceStorage();
        [JsonIgnore]
        public ResourceStorage RequeredResources { get; set; } = new ResourceStorage();
        [JsonIgnore]
        public List<Flow> Flows { get; set; } = new List<Flow>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Дефицит бюджета.</returns>
        public ResourceStorage GetResourceSheet()
        {
            return ExistingResource - GetRequeredResource();
        }

        public ResourceStorage GetExistingResource()
        {
            var ExistingResource = new ResourceStorage();
            foreach (var proj in SubProjects)
            {
                if (!IsExistSubProjects())
                {
                    foreach (var res in proj.Value.ExistingResource)
                    {
                        ExistingResource.Add(res);
                    }
                }
                else
                {
                    ExistingResource.Add(proj.Value.GetExistingResource());
                }
            }
            return ExistingResource.Sum();
        }

        public ResourceStorage GetExistingResource(DateTime date)
        {
            var ExistingResource = new ResourceStorage();
            foreach (var proj in SubProjects)
            {
                if (!IsExistSubProjects())
                {
                    foreach (var res in proj.Value.ExistingResource)
                    {
                        ExistingResource.Add(res);
                    }
                }
                else
                {
                    ExistingResource.Add(proj.Value.GetExistingResource(date));
                }
            }
            return ExistingResource.GetResources(date);
        }

        private bool IsExistSubProjects()
        {
            return (this.SubProjects == null || this.SubProjects.Count == 0) ? false : true;
        }

        public ResourceStorage GetRequeredResource()
        {
            var RequeredResources = new ResourceStorage();

            if (!IsExistSubProjects())
            {
                foreach (var res in this.RequeredResources)
                {
                    RequeredResources.Add(res);
                }
                return RequeredResources.Sum();
            }
            else
            {
                foreach (var proj in this.SubProjects)
                {

                    RequeredResources.Add(proj.Value.GetRequeredResource());
                }
                return RequeredResources.Sum();
            }

                      
      
        }

        public ResourceStorage GetRequeredResource(DateTime date)
        {
            var RequeredResources = new ResourceStorage();

            foreach (var proj in SubProjects)
            {
                if (!IsExistSubProjects())
                {
                    foreach (var res in proj.Value.RequeredResources)
                    {
                        RequeredResources.Add(res);
                    }
                }
                else
                {
                    RequeredResources.Add(proj.Value.GetRequeredResource());
                }
            }
            return RequeredResources.GetResources(date);
        }
        #endregion

        #region Ограничения
        [JsonIgnore]
        public List<Bound> HardBounds { get; set; }
        [JsonIgnore]
        public List<Bound> SoftBounds { get; set; }
        [JsonIgnore]
        public List<Bound> Bounds { get; set; }

        #endregion

        #region Signals

        [JsonIgnore]
        public SignalOptions SignalOptions { get; set; }
        #endregion

       


        public DateTime Start { get; set; }

        /// <summary>
        /// -1 запрет.
        /// 0 бесплатный сдвиг.
        /// Стоимость сдвига
        /// </summary>
        public long Price { get; set; }

        public TimeSpan DurationAvg { get; set; }

        /// <summary>
        /// 0 работа может быть сведена к нулевой длительности.
        /// n длительность в днях, n принадлежит N, n>0, n<"длительности нормально".
        /// </summary>
        public TimeSpan DurationMin { get; set; }

        /// <summary>
        /// -1 запрет уменьшения
        /// 0 бесплатное уменьшение
        /// n стоимость в рублях за каждый день сдвига.
        /// </summary>
        public long PriceDuration { get; set; }


        #region Вехи
        public List<Stage> Stages { get; set; } = new List<Stage>();


        #endregion




    }
    public class SignalOptions
    {
        public string Start
        {
            get;
            set;
        }
        public string End
        {
            get;
            set;
        }
        public string Pause
        {
            get;
            set;
        }
        public string Error
        {
            get;
            set;
        }
        public string Warning
        {
            get;
            set;
        }
        public string Stop
        {
            get;
            set;
        }
        public string Restart
        {
            get;
            set;
        }
        public override string ToString()
        {
            return $"{Start}\n{End}\n{Pause}";

        }
    }
      
   
    

  
   
}
