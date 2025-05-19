using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
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
        [Inject] readonly ICommandPublisher _publisher;
        [Inject] readonly CubeColorPresenter _cubeColorPresenter = default;

        /// <summary>
        /// キューブの親オブジェクト
        /// </summary>
        [SerializeField] GameObject cubeParent = null;

        private MRubyContext _mRubyContext;
        private CubeJsonExecutor _cubeJsonExecutor;

        void Awake()
        {
            // キューブの親オブジェクトがnullの場合はエラー
            Assert.IsNotNull(cubeParent, $"{nameof(cubeParent)} is null.");
            _cubeColorPresenter.CubeParentTransform = cubeParent.transform;
            // JSONアニメーション実行クラスの初期化
            _cubeJsonExecutor = new CubeJsonExecutor(_publisher, async () =>
            {
                var loadObj = await Resources.LoadAsync<TextAsset>("json/rgb").ToUniTask();
                var json = loadObj as TextAsset;
                return json == null || string.IsNullOrEmpty(json.text)
                    ? null
                    : JsonConvert.DeserializeObject<CubeJson>(json.text);
            });
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

            // JSON定義のアニメーションを実行
            await _cubeJsonExecutor.ExecuteAsync(destroyCancellationToken);
        }

        void OnDestroy()
        {
            _mRubyContext?.Dispose();
        }
    }
}
