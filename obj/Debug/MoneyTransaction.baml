   M S B A M L   `   `   `  ����  WPFWatermarking&     WPFWatermarking.MoneyTransaction   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      -   5        $	��488��5      $	��543��6     � 	Resources  5         CloseBtnStyle �� 
TargetType �� Property �� Value �� 
TargetType �� RadiusX �� RadiusY �Name � Points	 �� Triggers
 P� Property P� Value �� 
TargetName��%g  &	        �� 5   
   " ��6&   .�5      ��  �@�Cursor$
 Hand��6'   �� 5       � ��$ 17��6)   �� 5	       � ��$ 17��6*   �� 5
       � ��$ 17��6&   �� 5       � ��$ 17��6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      � .+�5      �� $ 1��6(   $ 1��64   
���   ����	#FFAAC5646M   � 5      $
 xBtn��6&   $��1,1,1,1q�64   ���	#FFAAC564m �   "i 7' P4  N  �B �5 К& ��O ��b �? @�3  �f "i `
O "i �0 � A  � "i $��Center=�6�   $��Center=�6�   5      	 5      P� 5      
 �@J�IsMouseOver . . �5   "   �� $
 xBtn��6)    � �� �	#FFFFFFFF6K   5      5      5      �� 5       � ��$ Right=�64   �� 5       � ��$	 Top=�62   �� 5       � ��$ 	0,10,10,0q�6'   5      5       .��5"      �� $��1,1,1,1q�6   ���	#FF333333��5$   
   �� 5%       �� EndPoint$ 0.4,0&�6"    �� 
StartPoint$ 0.4,1&�63   .��5&      �  � Color$ 	#FFD8ECFF��6    � Offset$ 0Q�61   � 5'      $ #1e0e06��6   $	 0.9Q�6/   � 5(      $ 	AliceBlue��6   $ 1Q�61   5)      ._�5+   
   � .+�5,      �� -   5         ��Name$ btnClose��5,      #��[  6'   $��80��6N   $��Close��6Y   
���  ��6�   �� 5-      -   5        $��
lbl_bankid��5-   t   $��14(�6   
��� ��$��28��6:   $
��Left=�6F   $��	10,49,0,0q�6a   $	��Top=�6�   $��64��6�   .��6�   	Bank Id6�   �� 5.      -   5        $��
cmb_bankid��5.   �   $��0q�6    �� DisplayMemberPath$ bankid��6+   $��23��6F   $��127,49,215,0q�6j    �� SelectedValuePath$ bankcode��6�   $	��Top=�6�   ��6R   ��  J� BitmapEffect 5/      O� 50      52      �� 53      -   5        $��	lbl_accID��53   u   $��14(�6   
��� ��$��28��6:   $
��Left=�6F   $��
10,117,0,0q�6a   $	��Top=�6�   $��83��6�   .��6�   
Account Id6�   �� 54      -   5        $��	txt_accid��54   L   $��23��6    �� 
IsReadOnly .  $��127,122,215,0q�65   $	��Top=�6]   �� 55      -   5        $��cb_getaccdetails��55   Y   
���**��$��14(�6(   $��26��66   $��227,159,215,0q�6B   $	��Top=�6q   .��6�   Get Details6�   �� 56      -   5        $��img_logo��56   '   $��	0,49,22,0q�6   $
��Fill=�67   $��Right=�6F   $	��174��6b   $	��150��6n   $	��Top=�6{   �� 57      -	   5        $��cb_watermarkedlogo��57   r   
���**��$��14(�6(   $��26��66   $��Right=�6B   $��
0,214,22,0q�6^   $	��Top=�6�   $	��130��6�   .��6�   Watermarked Logo6�   )  #Microsoft.Windows.Controls.DataGrid  58      6my-http://schemas.microsoft.com/wpf/2008/toolkit  6#  -
   5        $��dg_viewAccounts��58   |     AutoGenerateColumns .  
�����p�
����� �   CanUserAddRows .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .     CanUserSortColumns .  !   EnableColumnVirtualization .     EnableRowVirtualization .  $��Arial)�6.  $��14(�6A  $��10,0,10,120q�6g     RowBackground
 � ���   RowDetailsVisibilityMode$ VisibleWhenSelected=�6�      VerticalGridLinesBrush
  ���_�"!   VerticalScrollBarVisibility$
! Auto=�6   $	��108��6\  $��Bottom=�6i  ��6O  ��  59      O� 5:      " O� 	GlowColor$" 	LightPink��6,   # O� GlowSize$# 10Q�6B   $   Columns$ 5<      3  -Microsoft.Windows.Controls.DataGridTextColumn  5=      %   
FontFamily$% Arial)�6J   &   FontSize$& 12(�6]   /  )Microsoft.Windows.Controls.DataGridColumn'   Header$' Sl.No��6k   (   
IsReadOnly( . )   Width8  2Microsoft.Windows.Controls.DataGridLengthConverter$) 60 6�   4  .Microsoft.Windows.Controls.DataGridBoundColumn*   Binding* 6,   �� + �� Path$
+ slno�6�     5>      $% Arial)�6K   $& 12(�6^   $' 
Account Id��6l   ( . ,   MaxWidth$, InfinityQ�6�   -   MinWidth$- 80Q�6�   $	) 200 6�   * 6,   �� $+ accid�6�     5?      $% Arial)�6S   $& 12(�6f   $' Holder Name��6t   ( . $, InfinityQ�6�   $- 80Q�6�   $	) 240 6�   * 6,   �� $+ accholdername�6�   5B      �� 5C      -   5        $��lbl_initialamount��5C   v   $��14(�6   
��� ��$��28��6:   $��Right=�6F   $��
0,0,154,80q�6b   $��Bottom=�6�   $��60��6�   .��6�   Amount6�   �� 5D      -   5        $��txt_amounttoTransfer��5D   d   $��23��6   $��Right=�6"    .  $��	0,0,10,85q�6Q   $��Bottom=�6�   $	��142��6�   �� 5E      -   5        $��cb_close��5E   q   
���**��$��14(�6(   $��26��66   $��Right=�6B   $��	0,0,10,18q�6^   $��Bottom=�6�   $��68��6�   .��6�   Close6�   �� 5F      -   5        $��cb_transferamount��5F   q   
���**��$��14(�6(   $��26��66   $��Right=�6B   $��	0,0,88,18q�6^   $��Bottom=�6�   $	��126��6�   .��6�   Transfer Amount6�   �� 5G      -   5        $��lbl_msg��5G   5   $��28��6   $��10,0,173,56q�6    $��Bottom=�6D   $��14(�6_    5H      O� 5I      $	" Red��6,   5K      �� 5L      -   5        $��lbl_fromaccId��5L   t   $��14(�6   
��� ��$��28��6:   $
��Left=�6F   $��	10,84,0,0q�6a   $	��Top=�6�   $	��117��6�   .��6�   From Account Id6�   �� 5M      -   5        $��txt_fromaccount��5M   K   $��23��6    .  $��127,85,215,0q�65   $	��Top=�6b   5N      5O      5P      