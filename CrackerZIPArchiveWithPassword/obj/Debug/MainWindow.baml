   M S B A M L   `   `   `  ����X  TCrackerZIPArchiveWithPassword, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:CrackerZIPArchiveWithPasswordCrackerZIPArchiveWithPassword  5   	   .    (CrackerZIPArchiveWithPassword.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   =local+clr-namespace:CrackerZIPArchiveWithPassword     5   	   -   5          � Title$'  !Cracker ZIP-archive with password��5   	   $	��214��63   $	��550��6@    � Icon$ Archive-icon.ico��5	   "    � 
ResizeMode$ NoResize=�6:   .��5      �  � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      $
��185* �6   �� 5      $
��340* �6   ��5   
   �� 5      .��5      �  � Color$ 	#FFE8CDCD��6    � Offset$ 0Q�61   � 5      $ 	#FF57544C��6   $ 1Q�61   5      .+�5   
   o� -   5         o�Name$ MainMenu��5      $��2��6!   .@�5       m� $
��File��6   .@�5!      m� -   5        $��MenuOpen��5!      $$��Open ZIP-archive with password��6+   6S   m� 5"      -   5        $��MenuSave��5"      $��Save state of search��6+   {�.  6d   m� 5#      -   5        $��
MenuSaveAs��5#      $!��Save state of search as ...��6-   {�.  6o   m� 5$      -   5        $��MenuOpenState��5$      $��Load state of search��60   6N   m� 5%      -   5        $��MenuExit��5%      $
��Exit��6+   69   5&      5'      �� 5)   
   -   5         ��Name$ label1��5)      $��1��6    $��5q�6-   $��File: ��68   $y�Hidden=�6I   �� 5*   
   -	   5        $ labelFileName��5*      $��1��6'   $��1��64   $��5q�6D   �� 5,   
   -
   5        $ label3��5,      $��3��6    $��Elapsed time:��6-   $��5q�6E   $y�Hidden=�6Q   �� 5-   
   -   5        $ labelElapsedTime��5-      $��3��6*   $��1��67   $��5q�6G   �� 5/   
   -   5        $ label2��5/      $��4��6    $��Current password: ��6-   $��5q�6J   $y�Hidden=�6U   �� 50   
   -   5        $ labelCurrPassword��50      $��4��6+   $��1��68   $��5q�6H   51      52      