![DL Count](https://img.shields.io/github/downloads/turtle-insect/DQ11/total.svg)
[![Build status](https://ci.appveyor.com/api/projects/status/88s05mxijfh619at?svg=true)](https://ci.appveyor.com/project/turtle-insect/dq11)

# 概要
3DS ドラゴンクエスト11のセーブデータ編集Tool

# ソフト
http://www.dq11.jp/

# 実行に必要
* Windows マシン
* .NET Framework 4.8の導入
* セーブデータの吸い出し
* セーブデータの書き戻し

# Build環境
* Windows 10(64bit)
* Visual Studio 2022

# 編集時の手順
* saveDataを吸い出す
   * 結果、以下が取得可能
      * Data0(Data1、Data2)
      * mini_0(mini_1、mini_2)
      * system
* Data0(Data1、Data2)を読み込む
* 任意の編集を行う
* Data0(Data1、Data2)を書き出す
* saveDataを書き戻す
