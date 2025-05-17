using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;
using VitalRouter;
using VitalRouter.MRuby;

namespace CubeDigit.Game
{
    /// <summary>
    /// アプリケーションのエントリーポイントとなるクラス
    /// </summary>
    public class EntryPoint : MonoBehaviour
    {
        [Inject] readonly Router _router = default;
        [Inject] readonly CubeColorPresenter _cubeColorPresenter = default;

        /// <summary>
        /// キューブの親オブジェクト
        /// </summary>
        [SerializeField] GameObject cubeParent = null;

        void Awake()
        {
            // キューブの親オブジェクトがnullの場合はエラー
            Assert.IsNotNull(cubeParent, $"{nameof(cubeParent)} is null.");
            _cubeColorPresenter.CubeParentTransform = cubeParent.transform;
        }

        async UniTaskVoid Start()
        {
            // コンテキストの生成
            var context = MRubyContext.Create();
            context.Router = _router;
            context.CommandPreset = new MyCommandPreset();

            // Rubyスクリプトの実行
            var rubySource = Resources.Load<TextAsset>("timeline");
            using MRubyScript script = context.CompileScript(rubySource.bytes);
            await script.RunAsync();
        }
    }
}
