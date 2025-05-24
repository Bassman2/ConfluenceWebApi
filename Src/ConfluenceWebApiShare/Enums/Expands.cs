namespace ConfluenceWebApi;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// '__' will be replaced with '.'
/// </remarks>
[Flags]

public enum Expands : ulong
{
    None = 0x0000,

    // range                               0x0000_0000_0000_0000_0000_0000_0000_000x 
    Ancestors                       /**/ = 0x0000_0000_0000_0000_0000_0000_0000_0001,

    // range                               0x0000_0000_0000_0000_0000_0000_0000_0xx0 
    Body                            /**/ = 0x0000_0000_0000_0000_0000_0000_0000_0010,
    Body__Editor                    /**/ = 0x0000_0000_0000_0000_0000_0000_0000_0020,
    Body__View                      /**/ = 0x0000_0000_0000_0000_0000_0000_0000_0040,
    Body__Export_View               /**/ = 0x0000_0000_0000_0000_0000_0000_0000_0080,
    Body__Styled_View               /**/ = 0x0000_0000_0000_0000_0000_0000_0000_0100,
    Body__Storage                   /**/ = 0x0000_0000_0000_0000_0000_0000_0000_0200,
    Body__Anonymous_Export_View     /**/ = 0x0000_0000_0000_0000_0000_0000_0000_0400,

    //Body__Storage__Content          /**/ = 0x0000_0000_0000_0001_0000_0000_0000_0000u,


    // range                               0x0000_0000_0000_x000                
    Children                        /**/ = 0x0000_0000_0000_1000,
    Children__Attachment            /**/ = 0x0000_0000_0000_2000,
    Children__Comment               /**/ = 0x0000_0000_0000_4000,
    Children__Page                  /**/ = 0x0000_0000_0000_8000,

    // range                               0x0000_0000_000x_0000                
    Container                       /**/ = 0x0000_0000_0001_0000,

    // range                               0x0000_0000_00x0_0000                
    Descendants                     /**/ = 0x0000_0000_0010_0000,
    Descendants__Attachment         /**/ = 0x0000_0000_0020_0000,
    Descendants__Comment            /**/ = 0x0000_0000_0040_0000,

    // range                               0x0000_0000_xx00_0000                
    History                         /**/ = 0x0000_0000_0100_0000,
    History__LastUpdated            /**/ = 0x0000_0000_0200_0000,
    History__PreviousVersion        /**/ = 0x0000_0000_0400_0000,
    History__Contributors           /**/ = 0x0000_0000_0800_0000,
    History__NextVersion            /**/ = 0x0000_0000_1000_0000,

    // range                               0x0000_00xx_0000_0000                
    Metadata                        /**/ = 0x0000_0001_0000_0000,
    Metadata__Currentuser           /**/ = 0x0000_0002_0000_0000,
    Metadata__Properties            /**/ = 0x0000_0004_0000_0000,
    Metadata__Frontend              /**/ = 0x0000_0008_0000_0000,
    Metadata__EditorHtml            /**/ = 0x0000_0010_0000_0000,
    Metadata__Labels                /**/ = 0x0000_0020_0000_0000,

    // range                               0x0000_0x00_0000_0000                
    Operations                      /**/ = 0x0000_0100_0000_0000,

    // range                               0x0000_x000_0000_0000                
    Restrictions                    /**/ = 0x0000_1000_0000_0000,
    Restrictions__Read              /**/ = 0x0000_2000_0000_0000,
    Restrictions__Update            /**/ = 0x0000_4000_0000_0000,

    // range                               0x00xx_0000_0000_0000                
    Space                           /**/ = 0x0001_0000_0000_0000,
    Space__Description              /**/ = 0x0002_0000_0000_0000,
    Space__Homepage                 /**/ = 0x0004_0000_0000_0000,
    Space__Icon                     /**/ = 0x0008_0000_0000_0000,
    Space__Metadata                 /**/ = 0x0010_0000_0000_0000,
    Space__RetentionPolicy          /**/ = 0x0020_0000_0000_0000,

    // range                               0x0x00_0000_0000_0000                
    Storage                         /**/ = 0x0100_0000_0000_0000,

    // range                               0xx000_0000_0000_0000                
    Version                         /**/ = 0x1000_0000_0000_0000,
    Version__Content                /**/ = 0x2000_0000_0000_0000,
}
