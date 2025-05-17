# agentに依頼する際の下書き
レスポンスの最初に「以下の指示を実行します」と記載し、その後 memo.md の内容を引用形式で出力してください。

CubeColorクラスを作成してください要件は以下です。EntryPointからはまだ使用しません。
- RGBに加えてcube表示しない場合の情報をboolで持つ
- alphaは持たない
- rgbとboolを引数に持つコンストラクタ
- RGBの部分をUnityEngine.Colorとして取得できる。
- UnityEngine.Colorからも変換できる。
- UnityEngine.Color.clearが指定された場合は非表示
- それ以外の場合はrgbの部分だけ保持

テストコードも実装してみてください。
