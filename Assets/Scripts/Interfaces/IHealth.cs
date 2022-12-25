namespace Interfaces
{
    public interface IHealth
    {
        public float Health { get; set; }

        public void ChangeHealth(float value);

    }
}