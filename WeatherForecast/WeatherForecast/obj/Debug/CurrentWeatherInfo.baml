   M S B A M L   `   `   `  ����  WeatherForecast(    "WeatherForecast.CurrentWeatherInfo   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      ��   �� Orientation$  Vertical=�6   
������ .+�5	   
   �� 
�������$��Center=�6�   $��36(�6�   ��6   ��  �� Path$/ )Weather.CurrentWeatherData.CityAndCountry� �� UpdateSourceTrigger$ PropertyChanged=�6�   � 5
   
    � RowDefinitions 5      �� 5       � ColumnDefinitions 5      �� 5      $
��1.2* �6#   �� 5      .+�5      �� $��80��6z   $��80��6�   $��10q�6�   $��0��6�   $��0��6�   ��6   �� $- 'Weather.CurrentWeatherData.WeatherImage�$ PropertyChanged=�6�   �� 5      
�������$��48(�6�   $��Bottom=�6�   $��0��6�   $��1��6�   ��6   �� $, &Weather.CurrentWeatherData.Temperature�$ PropertyChanged=�6�   5      �� 5   
   
�������$��Center=�6�   $��18(�6�   ��6   �� $, &Weather.CurrentWeatherData.Description�$ PropertyChanged=�6�   5      5      