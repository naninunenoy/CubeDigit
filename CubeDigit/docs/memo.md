# agentに依頼する際の下書き
さて、まずはEntryPointに以下の入力を受けてシーン上にcubeを配置するScriptを作成してください。
入力は[SerializeField]で受け取るようにしてください。

入力：
cubeの個数をVector3Intで受け取る.
すべてのcubeに反映する色をColorで受け取る.

出力：
指定された個数のcube(PrimitiveType.Cube)を等間隔に配置する.
配置の中心は原点(0,0,0)とする.
cubeのサイズは1.0fとする.これはconst floatで定義する.
cube同士の間隔は0.1fとする.これはconst floatで定義する.
