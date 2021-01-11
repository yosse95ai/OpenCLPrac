# OpenCLPrac

- [__OpenCLの日本語版リファレンス__](http://neareal.net/index.php?Programming%2FOpenCL%2FJpnReference)
- [__参考書__](https://www.amazon.co.jp/C-%E3%81%AB%E3%82%88%E3%82%8BOpenCL%E4%B8%A6%E5%88%97%E3%83%97%E3%83%AD%E3%82%B0%E3%83%A9%E3%83%9F%E3%83%B3%E3%82%B0-%E5%8C%97%E5%B1%B1-%E6%B4%8B%E5%B9%B8/dp/4877832971)

CONCEPT: C# × OpenCL

---

## 1-1 プロフィールを表示することプログラム
### OpenCLPrac1_1_1

コンピュータ内のプラットフォームを列挙し, それぞれのプラットフォーム内のデバイスを列挙.

そしていくつかの情報を表示.

表示する情報は多数あるため, 適当に選択.

---

## 3-1 配列乗算プログラム
### OpenCLPrac3_1_1

配列Aと配列Bの各要素を乗算し, 配列Cの対応する要素に格納.

### OpenCLPrac3_1_2

[OpenCLPrac3_1_1](#openclprac3_1_1)をOpenCLを用いてプログラムした場合.


```fish
$ diff result/OpenCLPrac3_1_1.txt result/OpenCLPrac3_1_2.txt 
$ #何も表示されない
```
## 3-2 カーネルプログラムの分離
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


\(カーネルは[kernel3_2_1](https://github.com/yosse95ai/OpenCLPrac/tree/master/kernel3_2_1)\)

## 3-3 データ並列
### OpenCLPrac3_3_1

これまでタスク並列だったものを, データ並列のプログラムに書き換えた.

カーネルとカーネル起動を要求するAPIがことなるだけ.

```fish
$ diff code/OpenCLPrac3_2_1.cs code/OpenCLPrac3_3_1.cs 
105c105,107
<         status = clEnqueueTask(queue, kernel, 0, null, IntPtr.Zero);
---
>         IntPtr[] globalSize = { (IntPtr)C.Length };
>         status = clEnqueueNDRangeKernel(queue, kernel, 1, null,
>             globalSize, null, 0, null, IntPtr.Zero);
```


\(カーネルは[kernel3_3_1](https://github.com/yosse95ai/OpenCLPrac/tree/master/kernel3_3_1)\)


データ並列で実行されるため, カーネルには各要素\(データ\)iに対する処理だけを記述.

このカーネルにiを与え, いくつも並列に動作させる.


## 3-4 カーネルの引数にコンスタント
### OpenCLPrac3_4_1

カーネルの引数にコンスタント(スカラ変数)を与える.

うまくいかなかったので [telatail](https://teratail.com/questions/315257?modal=q-comp)にて質問中.

\(カーネルは[kernel3_4_1](https://github.com/yosse95ai/OpenCLPrac/tree/master/kernel3_4_1)\)


---
## 4-1 マネージプログラムとアンマネージプログラム間のデータ交換
### OpenCLPrac4_1_1
__C#とDLLのデータ型対応表__

| C#<br>マネージ | .NET Framework表現<br>マネージ | アンマネージ                  |
| :------------- | :----------------------------: | :---------------------------- |
| bool           |            Boolean             | int<br>long                   |
| byte           |              Byte              | unsigned char                 |
| char           |              Char              | char                          |
| short          |             Int16              | short                         |
| ushort         |             UInt16             | unsigned short                |
| int            |             Int31              | int<br> long                  |
| uint           |             UInt32             | unsigned int<br>unsigned long |
| long           |             Int64              | long long                     |
| float          |             Signal             | float                         |
| double         |             Double             | double                        |
| byte\[\]       |            Byte\[\]            | unsigned char*                |
| T\[\]          |             T\[\]              | T*                            |
| ulong          |             Uint64             | size_t                        |
| -              |       Text.StringBuilder       | char*                         |
| string         |             String             | const char*                   |

## 4-2 マネージからアンマネージへデータを渡す
### OpenCLPrac4_2_1

C#のChar型とC/C++のcharは異なる.

これ以降, [__dll_Source__](https://github.com/yosse95ai/OpenCLPrac/tree/master/dll_Source) にdllのもととなるC++のソースコードを入れる.

### OpenCLPrac4_2_2

`int`, `uint`, `short`, `long`について動作を確認.

### OpenCLPrac4_2_3

浮動小数点型をマネージからアンマネージへ渡す.

`float`, `Single`, `double`型を渡す.

## 4-3 アンマネージからマネージへデータを返す