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

        MRubyContext _mRubyContext;

        void Awake()
        {
            // キューブの親オブジェクトがnullの場合はエラー
            Assert.IsNotNull(cubeParent, $"{nameof(cubeParent)} is null.");
            _cubeColorPresenter.CubeParentTransform = cubeParent.transform;
        }

        async UniTaskVoid Start()
        {
            // コンテキストの生成
            _mRubyContext = MRubyContext.Create();
            _mRubyContext.Router = _router;
            _mRubyContext.CommandPreset = new MyCommandPreset();

            // Rubyスクリプトの実行
            /*var rubySource = Resources.Load<TextAsset>("mruby/main");
            using MRubyScript script = _mRubyContext.CompileScript(rubySource.bytes);
            await script.RunAsync();*/
        }

        void OnDestroy()
        {
            _mRubyContext?.Dispose();
        }
    }
}
