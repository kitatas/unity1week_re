using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Re.InGame.Presentation.View
{
    public sealed class StageView : MonoBehaviour
    {
        [SerializeField] private GoalView goalView = default;
        [SerializeField] private StageObjectView[] stageObjectViews = default;

        public async UniTask AppearAsync(float animationTime, CancellationToken token)
        {
            var tasks = new List<UniTask>();
            foreach (var stageObjectView in stageObjectViews)
            {
                tasks.Add(stageObjectView.AppearAsync(animationTime, token));
            }

            if (tasks.Count > 0)
            {
                await (
                    goalView.AppearAsync(token),
                    UniTask.WhenAll(tasks)
                );
            }
            else
            {
                await (
                    goalView.AppearAsync(token)
                );
            }
        }

        public async UniTask DisappearAsync(float animationTime, CancellationToken token)
        {
            var tasks = new List<UniTask>();
            foreach (var stageObjectView in stageObjectViews)
            {
                tasks.Add(stageObjectView.DisappearAsync(animationTime, token));
            }

            if (tasks.Count > 0)
            {
                await (
                    UniTask.WhenAll(tasks)
                );
            }
        }
    }
}