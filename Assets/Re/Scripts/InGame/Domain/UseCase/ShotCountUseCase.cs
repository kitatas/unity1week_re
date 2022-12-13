using Re.Common.Domain.UseCase;
using Re.InGame.Data.Entity;

namespace Re.InGame.Domain.UseCase
{
    public sealed class ShotCountUseCase : BaseModelUseCase<int>
    {
        private readonly ShotCountEntity _shotCountEntity;

        public ShotCountUseCase(ShotCountEntity shotCountEntity)
        {
            _shotCountEntity = shotCountEntity;
            Set(_shotCountEntity.value);
        }

        public void Increase()
        {
            _shotCountEntity.Increase();
            Set(_shotCountEntity.value);
        }
    }
}