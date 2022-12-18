using System;
using Re.OutGame.Domain.Repository;
using UniRx;
using UnityEngine;

namespace Re.OutGame.Domain.UseCase
{
    public sealed class SoundUseCase
    {
        private readonly SoundRepository _soundRepository;
        private readonly ReactiveProperty<AudioClip> _playBgm;
        private readonly Subject<AudioClip> _playSe;
        private readonly ReactiveProperty<float> _bgmVolume;
        private readonly ReactiveProperty<float> _seVolume;

        public SoundUseCase(SoundRepository soundRepository)
        {
            _soundRepository = soundRepository;
            _playBgm = new ReactiveProperty<AudioClip>();
            _playSe = new Subject<AudioClip>();
            _bgmVolume = new ReactiveProperty<float>(0.5f);
            _seVolume = new ReactiveProperty<float>(0.5f);
        }

        public IObservable<AudioClip> PlayBgm() => _playBgm;
        public IObservable<AudioClip> PlaySe() => _playSe;
        public IReadOnlyReactiveProperty<float> UpdateBgmVolume() => _bgmVolume;
        public IReadOnlyReactiveProperty<float> UpdateSeVolume() => _seVolume;
        public float bgmVolume => _bgmVolume.Value;
        public float seVolume => _seVolume.Value;

        public void SetUpPlayBgm(BgmType type)
        {
            var clip = _soundRepository.Find(type).clip;
            if (clip == null)
            {
                throw new Exception($"bgm clip is null. (type: {type})");
            }

            _playBgm.Value = clip;
        }

        public void SetUpPlaySe(SeType type)
        {
            var clip = _soundRepository.Find(type).clip;
            if (clip == null)
            {
                throw new Exception($"se clip is null. (type: {type})");
            }

            _playSe?.OnNext(clip);
        }

        public void SetBgmVolume(float value)
        {
            _bgmVolume.Value = value;
        }

        public void SetSeVolume(float value)
        {
            _seVolume.Value = value;
        }
    }
}