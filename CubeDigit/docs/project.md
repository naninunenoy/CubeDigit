# プロジェクト概要
このプロジェクトは、UnityとC#を用いた「CubeDigit」の開発リポジトリです。

## CubeDigitとは
指定された個数で同じサイズのcubeを等間隔に配置し、指定された色に変更するシステムとそのファイル定義です。
例えば (2,2,2) で指定された場合は8個のcubeが配置されます。
各cubeは(x,y,z)の座標をで一意に識別されます。
(x,y,z)のcubeの色を #AABBCC のようなカラーコードで指定します。

## ファイル例
以下のような外部ファイルを読み込むこんで表示に反映します。
```json
{
  "settings" : {
    "cubeSize": 1,
    "cubeSpacing": 0.5,
    "cubeNumber": { "x": 2, "y": 2, "z": 2 }
  },
  "cubes" : {
    "0|0|0": "#AABBCC",
    "1|0|0": "#AABBCC",
    "0|1|0": "#AABBCC",
    "1|1|0": "#AABBCC",
    "0|0|1": "#AABBCC",
    "1|0|1": "#AABBCC",
    "0|1|1": "#AABBCC",
    "1|1|1": "#AABBCC"
  }
}
```

## 使用技術
- Unity 6000.0.34
- C#

## ディレクトリ構成
**Assets/ Packages/ ProjectSettings/ 以外のディレクトリの中は参照も変更もしないでください**
- Assets/ : Unityプロジェクトのメインディレクトリ
  - CubeDigit/Scripts/Game/ : ゲームロジックのC#スクリプト置き場
  - CubeDigit/Scripts/Tests/EditMode/ : ゲームロジックのテストコードの置き場
  - CubeDigit/Prefabs/ : プレハブデータ
  - CubeDigit/Scenes/ : シーンデータ
- Packages/ : なんのライブラリが使われているか
- ProjectSettings/ : プロジェクトの設定
