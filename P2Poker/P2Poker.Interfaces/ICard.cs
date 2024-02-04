namespace P2Poker.Interfaces;

public interface ICard
{
    Guid UUID { get; set; }
    int value { get; set; }
}