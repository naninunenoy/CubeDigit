# agentに依頼する際の下書き
レスポンスの最初に「以下の指示を実行します」と記載し、その後 memo.md の内容を引用形式で出力してください。

色コードを指定して CubeColor を生成する CubeColorGenerator を実装してください。
bool TryFromHex(string hex, out CubeColor color) メソッドを実装してください。
hex は #RRGGBB 形式の文字列で、色を表します。
"#" のみが指定された場合はVisible=falseのCubeColorを返してください。
対応するテストコードの実装してください。
