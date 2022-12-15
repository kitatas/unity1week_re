using Re.Common.Domain.UseCase;
using Re.InGame.Data.Entity;

namespace Re.InGame.Domain.UseCase
{
    public sealed class BackCountUseCase : BaseModelUseCase<int>
    {
        private readonly BackCountEntity _backCountEntity;

        public BackCountUseCase(BackCountEntity backCountEntity)
        {
            _backCountEntity = backCountEntity;
            Set(_backCountEntity.value);
        }

        public void Increase()
        {
            _backCountEntity.Increase();
            Set(_backCountEntity.value);
        }
    }
}