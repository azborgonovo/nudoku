namespace Nudoku.Engine;

public interface IPuzzleGenerator
{
    IGrid Generate(int size);
    IGrid Generate(int boxWidth, int boxHeight);
}