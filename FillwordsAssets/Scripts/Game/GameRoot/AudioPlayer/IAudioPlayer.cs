namespace FillWords.Root.Audio
{
    public interface IAudioPlayer<TClip>
    {
        public void Init();
        public void SetBGMusic(TClip music);
        public void PlaySFX(TClip clip);
    }
}
