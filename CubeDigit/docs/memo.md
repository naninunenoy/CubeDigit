# agentに依頼する際の下書き
レスポンスの最初に「以下の指示を実行します」と記載し、その後 memo.md の内容を引用形式で出力してください。

EntryPointのcube群を生成する処理を CubeGenerator というクラスに分離してください。
Generateというメソッドを実装してください。
引数以下
- x, y, zの各サイズ
- cubeのサイズ
- cubeの間隔
- cubeの親となるTransform
返り値を ICubeRenderer としてください
