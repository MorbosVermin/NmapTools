package com.waitwha.util;

import java.util.Arrays;

/**
 * <b>NessusTools</b>: ArrayUtils<br/>
 * <small>Copyright (c)2013 Mike Duncan &lt;<a href="mailto:mike.duncan@waitwha.com">mike.duncan@waitwha.com</a>&gt;</small><p />
 *
 * <pre>
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 * </pre>
 *
 * TODO Document this class/interface.
 *
 * @author Mike Duncan <mike.duncan@waitwha.com>
 * @version $Id$
 * @package com.waitwha.util
 */
public class ArrayUtils {

	/**
	 * Concats the given arrays into a single array.
	 * 
	 * @param arr0	First array.
	 * @param more	Additional arrays to merge/concat onto the first given.
	 * @return Merged array
	 */
	public static final <T> T[] concat(T[] arr0, @SuppressWarnings("unchecked") T[]...more)  {
		int size = arr0.length;
		for(T[] a : more)
			size += a.length;
		
		T[] r = Arrays.copyOf(arr0, size);
		int o = arr0.length;
		for(T[] a : more)  {
			System.arraycopy(a, 0, r, o, a.length);
			o += a.length;
		}
		
		return r;
	}
	
	/**
	 * Merges the given Collection split into a String with the delimiter given.
	 * 
	 * @param arr0
	 * @param delimiter
	 * @return
	 */
	public static final <T> String merge(T[] arr0, String delimiter)  {
		StringBuffer buffer = new StringBuffer();
		for(T t : arr0)
			buffer.append(t + delimiter);
			
		return buffer.toString().substring(0, buffer.length() - delimiter.length());
	}
	
}
