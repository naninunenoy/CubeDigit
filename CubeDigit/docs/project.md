# プロジェクト概要
このプロジェクトは、UnityとC#を用いた「CubeDigit」の開発リポジトリです。

## CubeDigitとは
指定された個数で同じサイズのcubeを等間隔に配置し、指定された色に変更するシステムとそのファイル定義です。
例えば (2,2,2) で指定された場合は8個のcubeが配置されます。
各cubeは(x,y,z)の座標をで一意に識別されます。
(x,y,z)のcubeの色を #AABBCC のようなカラーコードで指定します。

# 使用技術
- Unity 6000.0.34
- C#

# ディレクトリ構成
- Assets/ : Unityプロジェクトのメインディレクトリ
  - CubeDigit/Scripts/Game/ : ゲームロジックのC#スクリプト置き場
  - CubeDigit/Scripts/Tests/EditMode/ : ゲームロジックのテストコードの置き場
  - CubeDigit/Prefabs/ : プレハブデータ
  - CubeDigit/Scenes/ : シーンデータ
