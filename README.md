# OpenCLPrac

- [__OpenCLの日本語版リファレンス__](http://neareal.net/index.php?Programming%2FOpenCL%2FJpnReference)
- [__参考書__](https://www.amazon.co.jp/C-%E3%81%AB%E3%82%88%E3%82%8BOpenCL%E4%B8%A6%E5%88%97%E3%83%97%E3%83%AD%E3%82%B0%E3%83%A9%E3%83%9F%E3%83%B3%E3%82%B0-%E5%8C%97%E5%B1%B1-%E6%B4%8B%E5%B9%B8/dp/4877832971)

---
## 1.1 プロフィールを表示することプログラム
### OpenCLPrac1_1_1

コンピュータ内のプラットフォームを列挙し, それぞれのプラットフォーム内のデバイスを列挙.

そしていくつかの情報を表示.

表示する情報は多数あるため, 適当に選択.

---
## 3.1 配列乗算プログラム
### OpenCLPrac3_1_1

配列Aと配列Bの各要素を乗算し, 配列Cの対応する要素に格納.

### OpenCLPrac3_1_2

[OpenCLPrac3_1_1](#openclprac3_1_1)をOpenCLを用いてプログラムした場合.

ちなみに

```fish
$ diff result/OpenCLPrac3_1_1.txt result/OpenCLPrac3_1_2.txt 
$ #何も表示されない
```
## 3.2 カーネルプログラムの分離
### OpenCLPrac3_2_1

ホストプログラムとカーネルプログラムを分離.

カーネルを変更したとしてもホストをリコンパイルする必要なし.

__cl__ フォルダはOpenCL共通ファイルが保存されている.

 | ファイル名     | 説明                                                                |
 | :------------- | :------------------------------------------------------------------ |
 | clConstants.cs | 共通ファイル. OPenCLに関する定数を定義.                             |
 | clStruct.cs    | 共通ファイル. OpenCLに関する構造体を定義.                           |
 | clDll.cs       | 共通ファイル. OpenCL APIに関する定義を行う.                         |
 | Program.cs     | 前節のMainに相当. <br>前節との相違はカーネルを外部から読み込むこと. |

## 3.3 データ並列