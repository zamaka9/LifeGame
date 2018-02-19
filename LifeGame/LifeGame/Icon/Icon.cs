using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame
{
    abstract class Icon
    {
        public abstract void Initialize();

        public abstract void Update();

        public abstract void Draw();

        public void React(int x, int y)
        {
            clickX = x;
            clickY = y;
            ClickFlag = true;
        }

        public static const int IconSize = 64;
        public static Land Land { get; set; }
        public static Drawer Drawer { get; set; }
        public int GraphicHandle { get; protected set; }
        protected int clickX;
        protected int clickY;
        protected bool ClickFlag;
    }
}
