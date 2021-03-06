import $ from 'jquery';
import { batchActions } from 'redux-batched-actions';
import * as types from './actionTypes';
import { set, updateItem } from './baseActions';

const section = 'artistEditor';

const artistEditorActionHandlers = {
  [types.SAVE_ARTIST_EDITOR]: function(payload) {
    return function(dispatch, getState) {
      dispatch(set({
        section,
        isSaving: true
      }));

      const promise = $.ajax({
        url: '/artist/editor',
        method: 'PUT',
        data: JSON.stringify(payload),
        dataType: 'json'
      });

      promise.done((data) => {
        dispatch(batchActions([
          ...data.map((artist) => {
            return updateItem({
              id: artist.id,
              section: 'artist',
              ...artist
            });
          }),

          set({
            section,
            isSaving: false,
            saveError: null
          })
        ]));
      });

      promise.fail((xhr) => {
        dispatch(set({
          section,
          isSaving: false,
          saveError: xhr
        }));
      });
    };
  },

  [types.BULK_DELETE_ARTIST]: function(payload) {
    return function(dispatch, getState) {
      dispatch(set({
        section,
        isDeleting: true
      }));

      const promise = $.ajax({
        url: '/artist/editor',
        method: 'DELETE',
        data: JSON.stringify(payload),
        dataType: 'json'
      });

      promise.done(() => {
        // SignaR will take care of removing the serires from the collection

        dispatch(set({
          section,
          isDeleting: false,
          deleteError: null
        }));
      });

      promise.fail((xhr) => {
        dispatch(set({
          section,
          isDeleting: false,
          deleteError: xhr
        }));
      });
    };
  }
};

export default artistEditorActionHandlers;
