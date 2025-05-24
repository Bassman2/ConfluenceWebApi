namespace ConfluenceWebApi;

[Flags]
public enum Expands : long
{
    None = 0x0000,

    // range                               0x0000_0000_0000_000x 
    Ancestors                       /**/ = 0x0000_0000_0000_0001,

    // range                               0x0000_0000_0000_0xx0 
    Body                            /**/ = 0x0000_0000_0000_0010,
    Body_Editor                     /**/ = 0x0000_0000_0000_0020,
    Body_View                       /**/ = 0x0000_0000_0000_0040,
    Body_Export_View                /**/ = 0x0000_0000_0000_0080,
    Body_Styled_View                /**/ = 0x0000_0000_0000_0100,
    Body_Storage                    /**/ = 0x0000_0000_0000_0200,
    Body_Anonymous_Export_View      /**/ = 0x0000_0000_0000_0400,               
                    
    // range                               0x0000_0000_0000_x000                
    Children                        /**/ = 0x0000_0000_0000_1000,
    Children_Attachment             /**/ = 0x0000_0000_0000_2000,
    Children_Comment                /**/ = 0x0000_0000_0000_4000,
    Children_Page                   /**/ = 0x0000_0000_0000_8000,

    // range                               0x0000_0000_000x_0000                
    Container                       /**/ = 0x0000_0000_0001_0000,

    // range                               0x0000_0000_00x0_0000                
    Descendants                     /**/ = 0x0000_0000_0010_0000,
    Descendants_Attachment          /**/ = 0x0000_0000_0020_0000,
    Descendants_Comment             /**/ = 0x0000_0000_0040_0000,

    // range                               0x0000_0000_xx00_0000                
    History                         /**/ = 0x0000_0000_0100_0000,
    History_LastUpdated             /**/ = 0x0000_0000_0200_0000,
    History_PreviousVersion         /**/ = 0x0000_0000_0400_0000,
    History_Contributors            /**/ = 0x0000_0000_0800_0000,
    History_NextVersion             /**/ = 0x0000_0000_1000_0000,

    // range                               0x0000_00xx_0000_0000                
    Metadata                        /**/ = 0x0000_0001_0000_0000,
    Metadata_Currentuser            /**/ = 0x0000_0002_0000_0000,
    Metadata_Properties             /**/ = 0x0000_0004_0000_0000,
    Metadata_Frontend               /**/ = 0x0000_0008_0000_0000,
    Metadata_EditorHtml             /**/ = 0x0000_0010_0000_0000,
    Metadata_Labels                 /**/ = 0x0000_0020_0000_0000,

    // range                               0x0000_0x00_0000_0000                
    Operations                      /**/ = 0x0000_0100_0000_0000,

    // range                               0x0000_x000_0000_0000                
    Restrictions                    /**/ = 0x0000_1000_0000_0000,
    Restrictions_Read               /**/ = 0x0000_2000_0000_0000,
    Restrictions_Update             /**/ = 0x0000_4000_0000_0000,

    // range                               0x00xx_0000_0000_0000                
    Space                           /**/ = 0x0001_0000_0000_0000,
    Space_Description               /**/ = 0x0002_0000_0000_0000,
    Space_Homepage                  /**/ = 0x0004_0000_0000_0000,
    Space_Icon                      /**/ = 0x0008_0000_0000_0000,
    Space_Metadata                  /**/ = 0x0010_0000_0000_0000,
    Space_RetentionPolicy           /**/ = 0x0020_0000_0000_0000,

    // range                               0x0x00_0000_0000_0000                
    Storage                         /**/ = 0x0100_0000_0000_0000,

    // range                               0xx000_0000_0000_0000                
    Version                         /**/ = 0x1000_0000_0000_0000,
    Version_Content                 /**/ = 0x2000_0000_0000_0000,
}
