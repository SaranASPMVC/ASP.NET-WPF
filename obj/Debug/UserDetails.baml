   M S B A M L   `   `   `  ����  WPFWatermarking!    WPFWatermarking.UserDetails   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      6dg-http://schemas.microsoft.com/wpf/2008/toolkit  5      -   5        $	��450��5      $	��400��6     � 	Resources  5         CloseBtnStyle �� 
TargetType �� Property �� Value �� 
TargetType �� RadiusX �� RadiusY �Name � Points	 �� Triggers
 P� Property P� Value �� 
TargetName��%g  &	        �� 5   
   " ��6&   .�5      ��  �@�Cursor$
 Hand��6'   �� 5	       � ��$ 17��6)   �� 5
       � ��$ 17��6*   �� 5       � ��$ 17��6&   �� 5       � ��$ 17��6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      � .+�5      �� $ 1��6(   $ 1��64   
���   ����	#FFAAC5646M   � 5      $
 xBtn��6&   $��1,1,1,1q�64   ���	#FFAAC564m �   "i 7' P4  N  �B �5 К& ��O ��b �? @�3  �f "i `
O "i �0 � A  � "i $��Center=�6�   $��Center=�6�   5      	 5      P� 5      
 �@J�IsMouseOver . . �5   "   �� $
 xBtn��6)    � �� �	#FFFFFFFF6K   5      5      5      �� 5       � ��$ Right=�64   �� 5       � ��$	 Top=�62   �� 5       � ��$ 	0,10,10,0q�6'   5       5!      .��5#      �� $��1,1,1,1q�6   ���	#FF333333��5%   
   �� 5&       �� EndPoint$ 0.4,0&�6"    �� 
StartPoint$ 0.4,1&�63   .��5'      �  � Color$ 	#FFD8ECFF��6    � Offset$ 0Q�61   � 5(      $ #1e0e06��6   $	 0.9Q�6/   � 5)      $ 	AliceBlue��6   $ 1Q�61   5*      ._�5,   
   � .+�5-      �� -   5         ��Name$ btnClose��5-      #��[  6'   $��80��6N   $��Close��6Y   
���  ��6�   )  #Microsoft.Windows.Controls.DataGrid  5.      -   5        $��dg_userDetails��5.      $��17,48,14,81q�6H      AutoGenerateColumns .  
�����p�
����� �   CanUserAddRows .     CanUserResizeColumns .     CanUserResizeRows .     CanUserSortColumns .     CanUserReorderColumns .  !   EnableColumnVirtualization .  $��14(�6Q  $��Arial)�6_     VerticalGridLinesBrush
 ���_�"   VerticalScrollBarVisibility$
 Auto=�6�     RowDetailsVisibilityMode$ VisibleWhenSelected=�6�     RowBackground
 � ���   EnableRowVirtualization .  ��60   ��  J� BitmapEffect 5/      O� 50       O� 	GlowColor$ 	LightPink��6,     O� GlowSize$  10Q�6B   !   Columns! 52      3  -Microsoft.Windows.Controls.DataGridTextColumn  53      /  )Microsoft.Windows.Controls.DataGridColumn"   Header$" Sl.No��6,   #   
IsReadOnly# . $   
FontFamily$$ Arial)�6N   %   FontSize$% 12(�6a   &   Width8  2Microsoft.Windows.Controls.DataGridLengthConverter$& 60 6o   4  .Microsoft.Windows.Controls.DataGridBoundColumn'   Binding' 6z   �� ( �� Path$
( slno�  54      $" Username��6,   # . $$ Arial)�6P   $% 12(�6c   )   MinWidth$) 80Q�6q   *   MaxWidth$* InfinityQ�6   $	& 250 6�   ' 6�   �� $( username�7  1Microsoft.Windows.Controls.DataGridCheckBoxColumn  55      $" Select��60   # . ' 6S   �� $( selected�58      �� 59      -   5        $��
lbl_status��59   4   $��28��6   $��
17,0,79,43q�6    $��Bottom=�6F   $��14(�6a   
��� ��6�   �� 5:      -   5        $��cb_close��5:   p   
���**��$��14(�6(   $��26��66   $��Right=�6B   $��0,0,14,8q�6^   $��Bottom=�6�   $��72��6�   .��6�   Close6�   �� 5;      -   5        $��	cb_approv��5;   o   
���**��$��14(�6(   $��26��66   $
��Left=�6B   $��17,0,0,8q�6]   $��Bottom=�6�   $��72��6�   .��6�   	Approve6�   5<      5=      5>      