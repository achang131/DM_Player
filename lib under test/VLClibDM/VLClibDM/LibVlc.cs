using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;

namespace VLClibDM
{
    [StructLayout(LayoutKind.Sequential, Pack=1)]
   public struct libvlc_exception_t {
        public int b_raised;
        public int i_code;
        [MarshalAs(UnmanagedType.LPStr)]
        public string psz_message;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
   public struct libvlc_module_description_t {
        [MarshalAs(UnmanagedType.LPStr)]
        public string psz_name;
        [MarshalAs(UnmanagedType.LPStr)]
        public string psz_shortname;
        [MarshalAs(UnmanagedType.LPStr)]
        public string psz_longname;
        [MarshalAs(UnmanagedType.LPStr)]
        public string psz_help;
        libvlc_module_description_t p_next;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public  struct libvlc_media_stats_t {
        
int 	i_read_bytes;
 
float 	f_input_bitrate;
 
int 	i_demux_read_bytes;
 
float 	f_demux_bitrate;
 
int 	i_demux_corrupted;
 
int 	i_demux_discontinuity;
 
int 	i_decoded_video;
 
int 	i_decoded_audio;
 
int 	i_displayed_pictures;
 
int 	i_lost_pictures;
 
int 	i_played_abuffers;
 
int 	i_lost_abuffers;
 
int 	i_sent_packets;
 
int 	i_sent_bytes;

float f_send_bitrate;
    }
    enum  	libvlc_event_e { 
  libvlc_MediaMetaChanged =0, libvlc_MediaSubItemAdded, libvlc_MediaDurationChanged, libvlc_MediaParsedChanged, 
  libvlc_MediaFreed, libvlc_MediaStateChanged, libvlc_MediaSubItemTreeAdded, libvlc_MediaPlayerMediaChanged =0x100, 
  libvlc_MediaPlayerNothingSpecial, libvlc_MediaPlayerOpening, libvlc_MediaPlayerBuffering, libvlc_MediaPlayerPlaying, 
  libvlc_MediaPlayerPaused, libvlc_MediaPlayerStopped, libvlc_MediaPlayerForward, libvlc_MediaPlayerBackward, 
  libvlc_MediaPlayerEndReached, libvlc_MediaPlayerEncounteredError, libvlc_MediaPlayerTimeChanged, libvlc_MediaPlayerPositionChanged, 
  libvlc_MediaPlayerSeekableChanged, libvlc_MediaPlayerPausableChanged, libvlc_MediaPlayerTitleChanged, libvlc_MediaPlayerSnapshotTaken, 
  libvlc_MediaPlayerLengthChanged, libvlc_MediaPlayerVout, libvlc_MediaPlayerScrambledChanged, libvlc_MediaPlayerESAdded, 
  libvlc_MediaPlayerESDeleted, libvlc_MediaPlayerESSelected, libvlc_MediaPlayerCorked, libvlc_MediaPlayerUncorked, 
  libvlc_MediaPlayerMuted, libvlc_MediaPlayerUnmuted, libvlc_MediaPlayerAudioVolume, libvlc_MediaPlayerAudioDevice, 
  libvlc_MediaPlayerChapterChanged, libvlc_MediaListItemAdded =0x200, libvlc_MediaListWillAddItem, libvlc_MediaListItemDeleted, 
  libvlc_MediaListWillDeleteItem, libvlc_MediaListEndReached, libvlc_MediaListViewItemAdded =0x300, libvlc_MediaListViewWillAddItem, 
  libvlc_MediaListViewItemDeleted, libvlc_MediaListViewWillDeleteItem, libvlc_MediaListPlayerPlayed =0x400, libvlc_MediaListPlayerNextItemSet, 
  libvlc_MediaListPlayerStopped, libvlc_MediaDiscovererStarted =0x500, libvlc_MediaDiscovererEnded, libvlc_VlmMediaAdded =0x600, 
  libvlc_VlmMediaRemoved, libvlc_VlmMediaChanged, libvlc_VlmMediaInstanceStarted, libvlc_VlmMediaInstanceStopped, 
  libvlc_VlmMediaInstanceStatusInit, libvlc_VlmMediaInstanceStatusOpening, libvlc_VlmMediaInstanceStatusPlaying, libvlc_VlmMediaInstanceStatusPause, 
  libvlc_VlmMediaInstanceStatusEnd, libvlc_VlmMediaInstanceStatusError 
}

    enum libvlc_video_adjust_option_t
    {
        libvlc_adjust_Enable = 0, libvlc_adjust_Contrast, libvlc_adjust_Brightness, libvlc_adjust_Hue,
        libvlc_adjust_Saturation, libvlc_adjust_Gamma
    }
    enum libvlc_audio_output_device_types_t
    {
        libvlc_AudioOutputDevice_Error = -1, libvlc_AudioOutputDevice_Mono = 1, libvlc_AudioOutputDevice_Stereo = 2, libvlc_AudioOutputDevice_2F2R = 4,
        libvlc_AudioOutputDevice_3F2R = 5, libvlc_AudioOutputDevice_5_1 = 6, libvlc_AudioOutputDevice_6_1 = 7, libvlc_AudioOutputDevice_7_1 = 8,
        libvlc_AudioOutputDevice_SPDIF = 10
    }
    enum libvlc_audio_output_channel_t
    {
        libvlc_AudioChannel_Error = -1, libvlc_AudioChannel_Stereo = 1, libvlc_AudioChannel_RStereo = 2, libvlc_AudioChannel_Left = 3,
        libvlc_AudioChannel_Right = 4, libvlc_AudioChannel_Dolbys = 5
    }
    enum libvlc_media_type_t
    {
        libvlc_media_type_unknown, libvlc_media_type_file, libvlc_media_type_directory, libvlc_media_type_disc,
        libvlc_media_type_stream, libvlc_media_type_playlist
    }
    enum libvlc_state_t
    {
        libvlc_NothingSpecial = 0, libvlc_Opening, libvlc_Buffering, libvlc_Playing,
        libvlc_Paused, libvlc_Stopped, libvlc_Ended, libvlc_Error
    }
    enum libvlc_meta_t
    {
        libvlc_meta_Title, libvlc_meta_Artist, libvlc_meta_Genre, libvlc_meta_Copyright,
        libvlc_meta_Album, libvlc_meta_TrackNumber, libvlc_meta_Description, libvlc_meta_Rating,
        libvlc_meta_Date, libvlc_meta_Setting, libvlc_meta_URL, libvlc_meta_Language,
        libvlc_meta_NowPlaying, libvlc_meta_Publisher, libvlc_meta_EncodedBy, libvlc_meta_ArtworkURL,
        libvlc_meta_TrackID, libvlc_meta_TrackTotal, libvlc_meta_Director, libvlc_meta_Season,
        libvlc_meta_Episode, libvlc_meta_ShowName, libvlc_meta_Actors, libvlc_meta_AlbumArtist,
        libvlc_meta_DiscNumber, libvlc_meta_DiscTotal
    }

    static class LibVlc
    {
        // not sure what vlclib_exception do, but add it in ref in everything except relase
        
        #region core
        [DllImport("libvlc")]
        public static extern IntPtr libvlc_new(int argc, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] argv);
        [DllImport("libvlc")]
        public static extern void libvlc_release(IntPtr instance);
        #endregion

        #region media
        [DllImport("libvlc")]
        public static extern IntPtr libvlc_media_new_location(IntPtr p_instance, [MarshalAs(UnmanagedType.LPStr)] string psz_mrl);
        //Create a media with a certain given media resource location(Url), for instance a valid URL.

        [DllImport("libvlc")]
        public static extern IntPtr libvlc_media_new_path(IntPtr p_instance,[MarshalAs(UnmanagedType.LPStr)] string psz_mrl);
        //Create a media with a certain given media resource location(local), for instance a valid URL.

        [DllImport("libvlc")]
        public static extern void libvlc_media_release(IntPtr media);//p_meta_desc
        //Decrement the reference count of a media descriptor object.


        [DllImport("libvlc")]
        public static extern int libvlc_media_get_stats(IntPtr media, ref libvlc_media_stats_t stats);
        //get the statistics if avaliable
        //return true if avaliable 0 otherwise
       
        [DllImport("libvlc")]
        public static extern libvlc_state_t libvlc_media_get_state(IntPtr media);
        //Get current state of media descriptor object.
        //Possible media states are libvlc_NothingSpecial=0, libvlc_Opening, libvlc_Playing, libvlc_Paused, libvlc_Stopped, libvlc_Ended, libvlc_Error.

        [DllImport("libvlc")]
        public static extern Int64 libvlc_media_get_duration(IntPtr media);
        //gets the duration of the media (in ms)

        [DllImport("libvlc")]
        public static extern uint libvlc_media_tracks_get(IntPtr p_md,ref IntPtr track);
        //gets the track info into the array, must played the video once for it to work
        //otherwise it will return empty array


        [DllImport("libvlc")]
        public static extern void libvlc_media_tracks_release(ref IntPtr track,uint count);
        //release track description
        //not doing this will result in an empty array
        //count is the number of elements in the array

        
        [DllImport("libvlc")]
        public static extern string libvlc_media_get_codec_description(IntPtr track_type, UInt32 track_fourcc);
        //get codec description from media elementary steam.

        
        [DllImport("libvlc")]
        public static extern IntPtr libvlc_media_event_manager(IntPtr media);
        //get the event manger from media descriptor

        
        [DllImport("libvlc")]
        public static extern string libvlc_media_get_mrl(IntPtr media);
        //get the string mrl of the media object
        //use this to get the current loaded media title/location?



        #endregion

        #region media player
        [DllImport("libvlc")]
        public static extern IntPtr libvlc_media_player_new(IntPtr instance);
        //create an empty Media Player Object

        [DllImport("libvlc")]
        public static extern IntPtr libvlc_media_player_new_from_media(IntPtr media);//p_md 
       //create a media player object from a media,
        //used when opening associated files ?

        [DllImport("libvlc")]
        public static extern void libvlc_media_player_pause(IntPtr player );
        //toggle pause(no effect if no media)

        [DllImport("libvlc")]
        public static extern void libvlc_media_player_play(IntPtr player );
        //play

        [DllImport("libvlc")]
        public static extern void libvlc_media_player_stop(IntPtr player );
        //stop the media

        [DllImport("libvlc")]
        public static extern void libvlc_media_player_set_media(IntPtr player, IntPtr media);
        //set the media, the media can be safly destroied after set

        [DllImport("libvlc")]
        public static extern IntPtr libvlc_media_player_get_media(IntPtr player );
        //get the current media in media player

        [DllImport("libvlc")]
        public static extern int libvlc_media_player_is_playing(IntPtr player );
        //check if player is playing 1 for playing and 0 for otherwise

        [DllImport("libvlc")]
        public static extern int libvlc_media_player_is_seekable(IntPtr player );
        // ture if the player can seek


        [DllImport("libvlc")]
        public static extern void libvlc_media_player_release(IntPtr player);
        //release the media_player after use 
        
        [DllImport("libvlc")]
        public static extern IntPtr libvlc_media_player_event_manager(IntPtr player);
        //get the event manager from the media player send event


        [DllImport("libvlc")]
        public static extern int libvlc_media_player_can_pause(IntPtr player );
        //ture if can pause

        [DllImport("libvlc")]
        public static extern IntPtr libvlc_media_player_get_length(IntPtr player );
        //get the current loaded movie length (in ms) -1 if no media


        [DllImport("libvlc")]
        public static extern Int64 libvlc_media_player_get_time(IntPtr player );
        //get the current movie time (in ms), return -1 if no media is set


        //[DllImport("libvlc")]
        //public static extern void libvlc_media_player_set_time(IntPtr player, [MarshalAs(UnmanagedType.I8)] Int64 time );
        [DllImport("libvlc")]
        public static extern void libvlc_media_player_set_time(IntPtr player, Int64 time );        
        //set the current time in media (in ms)

        [DllImport("libvlc")]
        public static extern float libvlc_media_player_get_position(IntPtr player );
        //get the current position in percentage betwen 0.0 and 1.0

        [DllImport("libvlc")]
        public static extern void libvlc_media_player_set_position(IntPtr player,  float f_pos);
        //set the movie position in percentage between 0.0 and 1.0
        //must enable playback for this to work

        [DllImport("libvlc")]
        public static extern libvlc_state_t libvlc_media_player_get_state(IntPtr player );
        //get the current player state(playing. . . etc)

        [DllImport("libvlc")]
        public static extern void libvlc_media_player_set_video_title_display(IntPtr player, IntPtr position, UInt64 timeout);
        //set if and how the video title will be shown when media played
        //position: position to display title libvlc_position_disable to prevent title from being displayed
        //timeout: title display timeout in ms (ignored if libvlc_position_disable)

        [DllImport("libvlc")]
        public static extern float libvlc_media_player_get_rate(IntPtr player);
        //get the movie play rate

        [DllImport("libvlc")]
        public static extern int libvlc_media_player_set_rate(IntPtr player, float rate);
        //set the movie play rate
        //-1 if error 0 otherwise, (might not work even if without getting -1)

        [DllImport("libvlc")]
        public static extern float libvlc_media_player_get_fps(IntPtr player);
        //get the movie fps don't support multi video tracks

        [DllImport("libvlc")]
        public static extern void libvlc_media_player_set_hwnd(IntPtr player, IntPtr drawable);
        //Set an X Window System drawable where the media player should render its video output.
        //tell it where should the media player to render it's video output
        //may need to ref drawable if not work

        [DllImport("libvlc")]
        public static extern void libvlc_media_player_set_nsobject(IntPtr player, IntPtr drawable);
        //set the nsview handler where the media should render it's video output
        //same as set_hwnd

        [DllImport("libvlc")]
        public static extern void libvlc_media_player_get_nsobject(IntPtr player);
        //get the NSview handler previously set with _set_nsobject

        #region Audio
        [DllImport("libvlc")]
        public static extern int libvlc_audio_set_volume(IntPtr player, int volume);
        //set the current software volume in percents
        //0=mute, 100 = 0dB according to the document


        [DllImport("libvlc")]
        public static extern void libvlc_audio_toggle_mute(IntPtr player );
        //toggle mute status
        //not work under some special conditions


        [DllImport("libvlc")]
        public static extern int libvlc_media_player_set_equalizer(IntPtr player, IntPtr equalizer);
        //need to create equalizer before use
        //libvlc_audio_equalizer_new() or libvlc_audio_equalizer_new_from_preset()
        //returns 0 on success and -1 on error



        [DllImport("libvlc")]
        public static extern IntPtr libvlc_audio_equalizer_new();
        //create a new default equalizer with all frequency values zeroed
        //returns null on error



        [DllImport("libvlc")]
        public static extern IntPtr libvlc_audio_equalizer_new_from_preset(uint u_index);
        //create a new equalizer with initial frequency values copies from presets
        //u_index is the index of the preset, counting from 0

        [DllImport("libvlc")]
        public static extern void libvlc_equalizer_release(IntPtr equalizer);
        //release a previously created equalizer instance
        //safe to invoke with a null equalizer for no effect



        [DllImport("libvlc")]
        public static extern int libvlc_audio_get_channel(IntPtr player );
        //get the current audio channel


        [DllImport("libvlc")]
        public static extern int libvlc_audio_get_mute(IntPtr player );
        //get the current mute status
        //-1 if undefined, boolean for the mute status



        [DllImport("libvlc")]
        public static extern int libvlc_audio_get_track(IntPtr player );
        //get the current audio track
        //returns the track ID -1 is no active input


        [DllImport("libvlc")]
        public static extern int libvlc_audio_get_track_count(IntPtr player );
        //get the total avaliable audio tracks
        //-1 if unavaliable, otherwise the total count of avaliable tracks


        [DllImport("libvlc")]
        public static extern int libvlc_audio_get_volume(IntPtr player );
        //get the current volume ( in percents)


        [DllImport("libvlc")]
        public static extern int libvlc_audio_set_track(IntPtr player, int track );
        //set the current audio track
        //returns 0 on sucess -1 on error


        [DllImport("libvlc")]
        public static extern int libvlc_audio_set_channel(IntPtr player, int channel );
        //set the current audio channel
        //0 on sucess -1 on error
        //
        //libvlc_AudioChannel_Error =-1
        //Stereo=1
        //RStereo=2
        //Left=3
        //Right =4
        //Dolbys=5

        [DllImport("libvlc")]
        public static extern int libvlc_audio_set_delay(IntPtr player, Int64 delay );
        //set current audio delay(in microseconds) will go back to 0 when media changes
        //0 on sucess


        [DllImport("libvlc")]
        public static extern Int64 libvlc_audio_get_delay(IntPtr player );
        //get the current media delay (in microseconds)


        [DllImport("libvlc")]
        public static extern IntPtr libvlc_audio_get_track_description(IntPtr player);
        //get the description of the audio tracks
        //return list of avaliable audio tracks description
        //or null must be freed with libvlc_track_description_list_release();


        [DllImport("libvlc")]
        public static extern void libvlc_track_description_list_release(IntPtr description);
        //release(free) libvlc_track_description_t.

        
        [DllImport("libvlc")]
        public static extern uint libvlc_audio_equalizer_get_preset_count();
        //get the number of equalizer presets


        [DllImport("libvlc")]
        public static extern String libvlc_audio_equalizer_get_preset_name(uint u_index);
        //get the name of a paticular equalizer preset
        #endregion

        #region Video

        [DllImport("libvlc")]
        public static extern int libvlc_video_get_height(IntPtr player);
        //get the current video height
        //out dated use size instead

        [DllImport("libvlc")]
        public static extern int libvlc_video_get_width(IntPtr player);
        //get the current video width
        //out dated use size instead


        [DllImport("libvlc")]
        public static extern int libvlc_video_get_size(IntPtr player, uint num,ref uint px,ref uint py);
        //get the pixel dimensions of a video
        //returns 0 on sucess -1 if the video doesn't exist

        
        [DllImport("libvlc")]
        public static extern void libvlc_video_set_deinterlace(IntPtr player, [MarshalAs(UnmanagedType.LPStr)] string psz_mode);
        //enable or disable deinterlace filter
        //psz_mode: type of deinterlace filter, null to disable


        [DllImport("libvlc")]
        public static extern void libvlc_video_set_key_input(IntPtr player, uint on);
        //enable or disable key press events according to the libvlc hotkeys config
        //on true to handle key press, false to ignore them
        //so probably 1 to enable 0 to disable ?


        [DllImport("libvlc")]
        public static extern int libvlc_video_get_track_count(IntPtr player);
        //get the number of available video tracks
        //returns the number of tracks


        [DllImport("libvlc")]
        public static extern int libvlc_video_get_track(IntPtr player);
        //get the current video track
        //will return current video track ID, -1 if no active input



        [DllImport("libvlc")]
        public static extern int libvlc_video_set_track(IntPtr player, int track);
        //set the video track
        //0 for sucess -1 for out of range


        [DllImport("libvlc")]
        public static extern void libvlc_video_set_mouse_input(IntPtr player, uint on);
        //enable or disable mouse click events handling
        //need for DVD menus and some vidoe filters like "puzzle"


        [DllImport("libvlc")]
        public static extern void libvlc_set_fullscreen(IntPtr player, int fullscreen);
        //enable or disable fullscreen
        //fullscreen 1 for true 0 for false won't work on  libvlic_media_player_set_xwindows()


        [DllImport("libvlc")]
        public static extern void libvlc_toggle_fullscreen(IntPtr player);
        //toggle fullscreen status on non-embeded video outputs
        //same limitation as set_fullscreen();


        [DllImport("libvlc")]
        public static extern int libvlc_get_fullscreen(IntPtr player);
        //get the current fullscreen status
        //returns bool for fullscreen status


        [DllImport("libvlc")]
        public static extern int libvlc_video_get_cursor(IntPtr player, uint num,ref int x,ref int y);
        //get the current cursor location on the rendering area
        //0 on sucess -1 when video doesn't exist

        
        [DllImport("libvlc")]
        public static extern float libvlc_video_get_scale(IntPtr player);
        //get the current video scalling factor
        //0 to fit the windows/drawable automatically

        
        [DllImport("libvlc")]
        public static extern void libvlc_video_set_scale(IntPtr player);
        //set the scalling of the video 0 to auto fit 
        //not all video supports this
        
        [DllImport("libvlc")]
        public static extern string libvlc_video_get_aspect_ratio(IntPtr player);
        //get the current video aspect ratio
        //null if unspecified the result must use libvlc_free();

        [DllImport("libvlc")]
        public static extern void libvlc_free(IntPtr ptr);
        //frees an heap allocation returned by a libvlc function
        //ptr is the pointer

        [DllImport("libvlc")]
        public static extern void libvlc_video_set_aspect_ratio(IntPtr player, [MarshalAs(UnmanagedType.LPStr)] string psz_aspect);
        //set new aspect ratio
        //invalid aspect will be ignored

        [DllImport("libvlc")]
        public static extern int libvlc_video_get_spu(IntPtr player);
        //get the current subtitle
        //-1 if none

        [DllImport("libvlc")]
        public static extern int libvlc_video_get_spu_count(IntPtr player);
        //get the totla number of subtitles avaliable


        [DllImport("libvlc")]
        public static extern IntPtr libvlc_video_get_spu_description(IntPtr player);
        // gets the descritption for the subtitles
        //must be released with libvlc_track_description_list_release()
        //libvlc_track_description_t
        //contains int id,string name and pointer libvlc_track_description_t to next item p_next


        [DllImport("libvlc")]
        public static extern int libvlc_video_set_spu(IntPtr player, int spu);
        //set the subtitle
        //returns 0 on sucess  and -1 out of range


        [DllImport("libvlc")]
        public static extern Int64 libvlc_video_get_spu_delay(IntPtr player);
        //get he current subtitle delay
        //positive means displayed later
        //negative means earlier
        //returnes times being delayed(in microsecond)


        [DllImport("libvlc")]
        public static extern int libvlc_video_set_spu_delay(IntPtr player, Int64 delay);
        //sets the times in microsecond to delay the subtitle display
        //will be reseted on video change
        //returns 0 on sucess -1 on error


        [DllImport("libvlc")]
        public static extern string libvlc_video_get_crop_geometry(IntPtr player);
        //get current crop filter geometry
        //returns null if unset


        [DllImport("libvlc")]
        public static extern void libvlc_video_set_crop_geometry(IntPtr player, [MarshalAs(UnmanagedType.LPStr)] string psz_geometry);
        //set new crop filter geometry
        //psz_geometry: null to unset
        //0 for enable libvlc_adjust_enable
        //1 for contrast
        //2 for brightness
        //3 for hue
        //4 for saturation
        //5 for gamma
        //	option values for libvlc_video_{get,set}_adjust_{int,float,bool}


        [DllImport("libvlc")]
        public static extern int libvlc_video_get_adjust_int(IntPtr player, libvlc_video_adjust_option_t option);
        //get the int value of the selecte option


        [DllImport("libvlc")]
        public static extern float libvlc_video_get_adjust_float(IntPtr player, libvlc_video_adjust_option_t option);
        //get the float value of the selected option


        [DllImport("libvlc")]
        public static extern void libvlc_video_set_adjust_int(IntPtr player, uint option, int value);
        //set adjust option as integer
        //options that takes different type of value are ignored
        //(arg !0)to enable adjust filter (arg 0) to stop


        [DllImport("libvlc")]
        public static extern void libvlc_video_set_adjust_float(IntPtr player, uint option, float value);
        //set adjust option as float
        //options takes differnt type value are ignored


        [DllImport("libvlc")]
        public static extern int libvlc_video_set_subtitle_file(IntPtr player, [MarshalAs(UnmanagedType.LPStr)] string subtitle_path);
        // set new video subtitle file
        //returns boolean for success status


        #endregion

        [DllImport("libvlc")]
        public static extern int libvlc_event_attach(IntPtr eventmanager, IntPtr eventtype, IntPtr callback, IntPtr user_data);
        //register an event notification
        //eventmanager = the event manager you want ot attached
        //usually obtained by vlc_my_object_event_manager(0 where my_object is the object you want to listen to

        //event type  the desired event you want to listen to
        //callback the function to call when the event occurs
        //userdata user provided data to carry with the event
        //returns 0 on success ENOMEM on error


        [DllImport("libvlc")]
        public static extern void libvlc_event_detach(IntPtr eventmanager, IntPtr eventtype, IntPtr callback, IntPtr userdata);
        //unregister the attached event notification


        [DllImport("libvlc")]
        public static extern string libvlc_event_type_name(IntPtr eventtype);
        //get event's type name


        #endregion

        #region events
        
        #endregion
        
        #region exception/error handling

        [DllImport("libvlc")]
        public static extern string libvlc_errmsg();
        //returns a human readable error message for the last libvlc error in the calling thread
        //null if no error
        [DllImport("libvlc")]
        public static extern void libvlc_clearerr();
        //clears the libvlc error status for the current thread
        //the error will be automaticly overridten if a new error comes up by default

        #endregion
        #region debug console
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        #endregion
    }
    class VlcInstance : IDisposable {
        internal IntPtr Handle;
        public VlcInstance()
        {

            Handle = LibVlc.libvlc_new(0, null);
            VlcException ex = new VlcException();
            // if any error it'll thow an exception here


        }
        public VlcInstance(String[] args) {

            Handle = LibVlc.libvlc_new(args.Length, args);
            VlcException ex = new VlcException();
            // if any error it'll thow an exception here
        
        
        }
        public void Dispose() {
            LibVlc.libvlc_release(Handle);
        }
    
    }
    class VlcMedia : IDisposable {
        internal IntPtr Handle;
        internal VlcInstance inst{
        set;get;   }
        public VlcMedia(VlcInstance instance, string url) {
            inst = instance;
            Handle = LibVlc.libvlc_media_new_path(instance.Handle, url);
            VlcException ex = new VlcException();
        
       
        }

        public VlcMedia(VlcInstance instance, Uri url)
        {
            inst = instance;
            Handle = LibVlc.libvlc_media_new_location(instance.Handle, url.AbsolutePath);
            VlcException ex = new VlcException();


        }
        public VlcMedia(IntPtr media) {

            Handle = media;

        }
        public String currentStatus {

            get {
                libvlc_state_t state =LibVlc.libvlc_media_get_state(Handle);
                
                VlcException ex = new VlcException();
                return state.ToString();
            }
        }

        public Int64 duration {
            get {
                
               Int64 du = LibVlc.libvlc_media_get_duration(Handle);
               VlcException ex = new VlcException();
                return du;
            }
        
        }

        public String currentMrl {
            get {
                
                string temp = LibVlc.libvlc_media_get_mrl(Handle);
                VlcException ex = new VlcException();
                return temp; }
        
        }
        public void Dispose() {
            LibVlc.libvlc_media_release(Handle);
        }

    }


    class VlcMediaPlayer : IDisposable {
        internal IntPtr Handle;
        private VlcInstance instance;
        
        private bool playing, paused;
        
        public IntPtr drawable { 
            get; 
            set { 
        LibVlc.libvlc_media_player_set_nsobject(Handle,value);
        VlcException ex = new VlcException();
            drawable=value;
        }}
        public VlcMediaPlayer(VlcInstance inst,IntPtr panel) {
            instance = inst;
            Handle = LibVlc.libvlc_media_player_new(inst.Handle);
            VlcException ex = new VlcException();
            drawable = panel;

        }
        public VlcMediaPlayer(VlcMedia media,IntPtr panel) {
            instance = media.inst;
            Handle = LibVlc.libvlc_media_player_new_from_media(media.Handle);
            VlcException ex = new VlcException();
            media.Dispose();
            drawable = panel;
        
        }
        public void setVlcMedia(Uri media) { 
            VlcMedia vlcm = new VlcMedia(instance,media);
            LibVlc.libvlc_media_player_set_media(Handle, vlcm.Handle);
            VlcException ex = new VlcException();
            vlcm.Dispose();
        
        }
        public void setVlcMedia(VlcMedia media){

            LibVlc.libvlc_media_player_set_media(Handle, media.Handle);
            media.Dispose();
            //media can be disposed after it's set
        
        }
        public void setVlcMedia(FileInfo media)
        {
            if (media.Exists)
            {
                VlcMedia vlcmedia = new VlcMedia(instance, media.FullName);

                setVlcMedia(vlcmedia);
            }
        }
        public void setVlcMedia(string path) {
            setVlcMedia(new FileInfo(path));
        }
        public string playerState
        {
            get
            {
                libvlc_state_t state = LibVlc.libvlc_media_player_get_state(Handle);
                VlcException ex = new VlcException();
                string result = state.ToString();
                return result;
            }
        }
        public bool isPlaying { get {  return playing && !paused; } }
        public bool isPaused { get { return playing && paused; } }
        public bool isStopped { get { return !playing; } }

        public void Play() {
            LibVlc.libvlc_media_player_play(Handle);
            VlcException ex = new VlcException();
            playing = true;
            paused = false;
        
        }
        public void Pause() {
            LibVlc.libvlc_media_player_pause(Handle);
            VlcException ex = new VlcException();
            if (playing)
            {
                paused = true;
            }
        }
        public void Stop() {
            LibVlc.libvlc_media_player_stop(Handle);
            VlcException ex = new VlcException();
            playing = false;
            paused = false;
        }
        public float FPS
        {
            get {
                float fps = LibVlc.libvlc_media_player_get_fps(Handle);
                VlcException ex = new VlcException();
                return fps;
            }
        }
        public void Dispose() {
            LibVlc.libvlc_media_player_release(Handle);
        
        }
        public Int64 VideoLength { get {
            Int64 length = LibVlc.libvlc_media_player_get_length(Handle).ToInt64();
            VlcException ex = new VlcException();
            return length;
        }}
        public Int64 currentVideoTime{get{

        Int64 time = LibVlc.libvlc_media_player_get_time(Handle);
        VlcException ex = new VlcException();
        return time;
        }
            set { 
            LibVlc.libvlc_media_player_set_time(Handle, value);
            VlcException ex = new VlcException();
            }
        }

       public VlcMedia getMedia() {
           VlcMedia temp = new VlcMedia(LibVlc.libvlc_media_player_get_media(Handle));
           VlcException ex = new VlcException();
           return temp;
        }

        
        
    
    }
     class VlcException : Exception
        {
            internal libvlc_exception_t ex;
            public VlcException()
                : base()
            {
                //ex = new libvlc_exception_t();

                string error = LibVlc.libvlc_errmsg();
                if (error != null)
                {
                    LibVlc.libvlc_clearerr();
                    string temp = error;
                    error = null;
                    throw new Exception(temp);
                }
            }

        }

    
}
