using System;
namespace FillWords.Root.SceneManagment
{
    public interface IScenesLoader
    {
        public void LoadScene(string sceneName, Action completeCallback = null, Action<float> progressUpdate = null);
    }
}
