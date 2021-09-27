using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public abstract class Character<T, R>
    {
        public int LifePoint { get; set; }
        public int MaxLifePoint { get; set; }

        public abstract Option SkillOption(T item);
        public abstract R Skill(T item);
        public abstract Option DrawCardOption<F>(F item);
        public abstract void DrawCard(int? target);
        public abstract Option ShootOption(int? target);
        public abstract void Shoot(int? target);

        //Gamen lesz event prop és ez állítja vissza azt. Event = teljes kör. Kártya megnézi hogy az aktuális embernél vagyunk e ha nem akkor tovább lép és hív.
        public abstract Option ShootedOption(/*Game*/);
        public abstract void Shooted(/*Game*/);
        public virtual void Heal() 
        {
            if (LifePoint < MaxLifePoint) LifePoint++;
        }

    }
}
