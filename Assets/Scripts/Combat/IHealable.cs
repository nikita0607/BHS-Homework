namespace BHSCamp
{
    //интерфейс для взаимодействиями с объектами, которым можно нанести урон
    public interface IHealable
    {
        void TakeHeal(int amount);
    }
}