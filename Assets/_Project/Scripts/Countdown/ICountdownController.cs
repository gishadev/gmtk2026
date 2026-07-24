using Cysharp.Threading.Tasks;

namespace gishadev.gmtk.Countdown
{
    public interface ICountdownController
    {
        UniTask StartCountdown();
    }
}