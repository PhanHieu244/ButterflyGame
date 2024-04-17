using ChuongCustom;

namespace _Game.ChuongScripts.Scripts.Runtime
{
    public class Revive : PowerUpButton<Revive>
    {
        public override int Amount => Data.Player.revive;
        public override void Active()
        {
            Data.Player.revive--;
            FetchData();
            ScreenManager.Instance.Back();
            Spawner.Instance.DestroyAllBalls();
        }
    }
}