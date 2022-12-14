using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Re.InGame.Presentation.Controller
{
    public sealed class StateController
    {
        private readonly List<BaseState> _states;

        public StateController(TitleState titleState, SetUpState setUpState, InputState inputState,
            JudgeState judgeState, BackState backState, GoalState goalState, ResultState resultState)
        {
            _states = new List<BaseState>
            {
                titleState,
                setUpState,
                inputState,
                judgeState,
                backState,
                goalState,
                resultState,
            };
        }

        public async UniTaskVoid InitAsync(CancellationToken token)
        {
            foreach (var state in _states)
            {
                state.InitAsync(token).Forget();
            }

            await UniTask.Yield(token);
        }

        public async UniTask<GameState> TickAsync(GameState state, CancellationToken token)
        {
            var currentState = _states.Find(x => x.state == state);
            if (currentState == null)
            {
                throw new Exception($"Can't find state. (state: {state})");
            }

            return await currentState.TickAsync(token);
        }
    }
}